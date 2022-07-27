using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;

public partial class MoveSystem : SystemBase
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
        float2 _target = new float2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
        Entities.WithAll<PlayerTag>().ForEach((Entity e, int entityInQueryIndex, ref MovementData move) =>
        {
            move.target = _target;
        }).ScheduleParallel();
        Entities.ForEach((Entity e, int entityInQueryIndex, ref PhysicsVelocity pVol, in MovementData move) =>
        {
            float coef;
            if (move.target.x != 0 && move.target.y != 0)
            {
                coef = 0.70710678118f;
            }
            else
            {
                coef = 1f;
            }
            float smoothMove = move.maxSpeed / move.smoothing;
            if (pVol.Linear.x < move.maxSpeed && move.target.x > 0.1f)
            {
                pVol.Linear.x += smoothMove * coef;
            }
            else if (pVol.Linear.x > -move.maxSpeed && move.target.x < -0.1f)
            {
                pVol.Linear.x -= smoothMove * coef;
            }
            else if (move.target.x < 0.1f && move.target.x > -0.1f)
            {
                if (pVol.Linear.x > smoothMove)
                {
                    pVol.Linear.x -= smoothMove * coef;
                }
                else if (pVol.Linear.x < -smoothMove)
                {
                    pVol.Linear.x += smoothMove * coef;
                }
                else
                {
                    pVol.Linear.x = 0f;
                }
            }
            if (pVol.Linear.x < -move.maxSpeed)
            {
                pVol.Linear.x = -move.maxSpeed;
            }
            if (pVol.Linear.x > move.maxSpeed)
            {
                pVol.Linear.x = move.maxSpeed;
            }

            if (pVol.Linear.z < move.maxSpeed && move.target.y > 0.1f)
            {
                pVol.Linear.z += smoothMove * coef;
            }
            else if (pVol.Linear.z > -move.maxSpeed && move.target.y < -0.1f)
            {
                pVol.Linear.z -= smoothMove * coef;
            }
            else if (move.target.y < 0.1f && move.target.y > -0.1f)
            {
                if (pVol.Linear.z > smoothMove)
                {
                    pVol.Linear.z -= smoothMove * coef;
                }
                else if (pVol.Linear.z < -smoothMove)
                {
                    pVol.Linear.z += smoothMove * coef;
                }
                else
                {
                    pVol.Linear.z = 0f;
                }
            }
            if (pVol.Linear.z > move.maxSpeed)
            {
                pVol.Linear.z = move.maxSpeed;
            }
            if (pVol.Linear.z < -move.maxSpeed)
            {
                pVol.Linear.z = -move.maxSpeed;
            }
            

        }).ScheduleParallel();
        _endSimECB.AddJobHandleForProducer(Dependency);
    }
}
