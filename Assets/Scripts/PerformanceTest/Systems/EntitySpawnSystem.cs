using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

public partial class EntitySpawnSystem : SystemBase
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

        Entities.WithAll<SpawnTag>().ForEach((Entity e, int entityInQueryIndex, ref SpawnerData spawnData, ref RandomData rand) =>
        {
            spawnData.timer -= deltaTime;
            if(spawnData.timer <= 0)
            {
                spawnData.timer = spawnData.delay;
                Entity newEntity = pECB.Instantiate(entityInQueryIndex, spawnData.box);
                pECB.SetComponent<Translation>(entityInQueryIndex, newEntity, new Translation { Value = new float3(rand.rng.NextFloat(-spawnData.range, spawnData.range), 
                    rand.rng.NextFloat(-spawnData.range, spawnData.range), rand.rng.NextFloat(-spawnData.range, spawnData.range)) } );
                pECB.SetComponent<RotationData>(entityInQueryIndex, newEntity, new RotationData
                {
                    rotationSpeed = rand.rng.NextFloat(-5f,5f)
                });
            }

        }).ScheduleParallel();
        _endSimECB.AddJobHandleForProducer(Dependency);
    }
}
