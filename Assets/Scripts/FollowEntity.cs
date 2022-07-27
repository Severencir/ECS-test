using Unity.Entities;
using UnityEngine;

public class FollowEntity : MonoBehaviour
{
    public CameraFocus camFoc;
}
public class FollowEntityConv : GameObjectConversionSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((FollowEntity followEntity) =>
        {
            followEntity.camFoc.entity = GetPrimaryEntity(followEntity.gameObject);
        });
    }
}
