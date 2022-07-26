using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Entities;
public class TextUpdate : MonoBehaviour
{
    public static int boxCount = 0;
    public TMP_Text textMesh;
    void Update()
    {
        textMesh.text = "Boxes: \n" + (Mathf.Max(boxCount,SpawnCube.count)).ToString();
    }
}
