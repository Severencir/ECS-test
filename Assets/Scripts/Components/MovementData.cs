using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct MovementData : IComponentData 
{
    public float maxSpeed;
    public float smoothing;
    public float2 target;
}
