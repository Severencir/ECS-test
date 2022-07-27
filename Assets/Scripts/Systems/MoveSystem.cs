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
        float deltaTime = Time.DeltaTime;
        float2 _target = new float2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
        Entities.WithAll<PlayerTag>().ForEach((ref MovementData move) =>
        {
            move.target = _target;
        }).ScheduleParallel();
        Entities.ForEach((Entity e, int entityInQueryIndex, ref PhysicsVelocity pVel, in MovementData move) =>
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
            float smoothMove = move.maxSpeed * coef / move.smoothing;
            if (pVel.Linear.x < move.maxSpeed * coef && move.target.x > 0.1f)
            {
                pVel.Linear.x += smoothMove * coef;
            }
            else if (pVel.Linear.x > -move.maxSpeed * coef && move.target.x < -0.1f)
            {
                pVel.Linear.x -= smoothMove * coef;
            }
            else if (move.target.x < 0.1f && move.target.x > -0.1f)
            {
                if (pVel.Linear.x > smoothMove)
                {
                    pVel.Linear.x -= smoothMove * coef;
                }
                else if (pVel.Linear.x < -smoothMove)
                {
                    pVel.Linear.x += smoothMove * coef;
                }
                else
                {
                    pVel.Linear.x = 0f;
                }
            }
            if (pVel.Linear.x < -move.maxSpeed * coef)
            {
                pVel.Linear.x = -move.maxSpeed * coef;
            }
            if (pVel.Linear.x > move.maxSpeed * coef)
            {
                pVel.Linear.x = move.maxSpeed * coef;
            }

            if (pVel.Linear.z < move.maxSpeed * coef && move.target.y > 0.1f)
            {
                pVel.Linear.z += smoothMove * coef;
            }
            else if (pVel.Linear.z > -move.maxSpeed * coef && move.target.y < -0.1f)
            {
                pVel.Linear.z -= smoothMove * coef;
            }
            else if (move.target.y < 0.1f && move.target.y > -0.1f)
            {
                if (pVel.Linear.z > smoothMove)
                {
                    pVel.Linear.z -= smoothMove * coef;
                }
                else if (pVel.Linear.z < -smoothMove)
                {
                    pVel.Linear.z += smoothMove * coef;
                }
                else
                {
                    pVel.Linear.z = 0f;
                }
            }
            if (pVel.Linear.z > move.maxSpeed * coef)
            {
                pVel.Linear.z = move.maxSpeed * coef;
            }
            if (pVel.Linear.z < -move.maxSpeed * coef)
            {
                pVel.Linear.z = -move.maxSpeed * coef;
            }
            

        }).ScheduleParallel();
    }
}
