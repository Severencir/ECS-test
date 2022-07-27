using Unity.Entities;

public partial class EntityCountSystem : SystemBase
{
    protected override void OnUpdate()
    {
        TextUpdate.boxCount = EntityManager.CreateEntityQuery(typeof(RotationData)).CalculateEntityCount();
    }
}
