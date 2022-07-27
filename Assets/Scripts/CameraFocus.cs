using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Transforms;
using Unity.Entities;

public class CameraFocus : MonoBehaviour
{
    public Entity entity;
    EntityManager manager;
    public GameObject origin;

    private void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void LateUpdate()
    {
        if (entity == Entity.Null)
            return;
        transform.position = manager.GetComponentData<Translation>(entity).Value;
        transform.LookAt(origin.transform);
        transform.Rotate(0, 180, 0);
    }
}
