using Unity.Entities;
using Unity.Mathematics;


public struct RandomData : IComponentData
{
    public Random rng;
}

namespace UnityEngine
{
    public class RandomDataInit : MonoBehaviour
    {
    }
    public class RandomDataStart : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((RandomDataInit input) =>
            {
                Entity entity = GetPrimaryEntity(input);
                DstEntityManager.AddComponentData(entity, new RandomData
                {
                    rng = new Unity.Mathematics.Random((uint)Random.Range(1, uint.MaxValue - 1))
                });
            });
        }
    }
}