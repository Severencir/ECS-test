using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;



public partial class SpawnerSystem : SystemBase
{
    private EndInitializationEntityCommandBufferSystem _endInitEcb;
    private EntityArchetype _archetype;
    
    protected override void OnCreate()
    {
        _endInitEcb = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
        Entities.ForEach((Entity e, ref RandomData rand) =>
        {
            rand.rng = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(1, uint.MaxValue-1));
        }).ScheduleParallel();
    }

    protected override void OnUpdate()
    {
        EntityCommandBuffer _ecb = _endInitEcb.CreateCommandBuffer();
        EntityQuery spawnerQuery = EntityManager.CreateEntityQuery(typeof(SpawnerData));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ecb.AddComponentForEntityQuery<SpawnTag>(spawnerQuery);
        }
        if (Input.GetKeyDown("q"))
        {
            _ecb.RemoveComponentForEntityQuery<SpawnTag>(spawnerQuery);
        }

    }
}
