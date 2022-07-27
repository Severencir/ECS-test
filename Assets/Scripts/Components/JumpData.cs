using Unity.Entities;

[GenerateAuthoringComponent]
public struct JumpData : IComponentData 
{
    public float jumpVelocity;
}
