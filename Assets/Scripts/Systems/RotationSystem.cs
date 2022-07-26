using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;


public partial class RotationSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        
        Entities.ForEach((Entity e,ref Rotation rotation, in RotationData rot) =>
        {
            rotation.Value = math.mul(rotation.Value, quaternion.AxisAngle(math.up(), rot.rotationSpeed * deltaTime));
        }).ScheduleParallel();
    }
    
}
