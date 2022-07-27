using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct SpawnerData : IComponentData
{
    public Entity box;
    public float delay;
    public float timer;
    public float range;
}

