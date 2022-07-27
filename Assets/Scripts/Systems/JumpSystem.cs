using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;

public partial class JumpSystem : SystemBase
{
    EndSimulationEntityCommandBufferSystem _endSimECB;
    protected override void OnCreate()
    {
        _endSimECB = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        EntityCommandBuffer.ParallelWriter pECB = _endSimECB.CreateCommandBuffer().AsParallelWriter();
        float deltaTime = Time.DeltaTime;
        BuildPhysicsWorld physicsWorld = World.GetExistingSystem<BuildPhysicsWorld>();
        CollisionWorld collisionWorld = physicsWorld.PhysicsWorld.CollisionWorld;
        float jumping = UnityEngine.Input.GetAxisRaw("Jump");
        float extraGrav = 9.81f * 2f;
        Entities.ForEach((ref PhysicsVelocity pVel, in JumpData jump, in Translation translation) =>
        {
            RaycastInput raycastInput = new RaycastInput()
            {
                Start = translation.Value - new float3(0, 1f, 0),
                End = translation.Value - new float3(0, 1.01f, 0),
                Filter = new CollisionFilter()
                {
                    BelongsTo = ~0u,
                    CollidesWith = 1u << 0,
                    GroupIndex = 0
                }
            };
            bool canJump = collisionWorld.CastRay(raycastInput, out RaycastHit hit);
            if (canJump && jumping > 0.1f)
            {
                pVel.Linear.y = jump.jumpVelocity;
            }
            if (pVel.Linear.y < 0.5)
            {
                pVel.Linear.y -= extraGrav * deltaTime ;
            }
        }).Schedule();
        _endSimECB.AddJobHandleForProducer(Dependency);
	}
    
}
