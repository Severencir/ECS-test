using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdate2 : MonoBehaviour
{
    public TMP_Text textMesh;
    float timer = 0;
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            textMesh.text = "FPS: " + (int)(1 / Time.deltaTime);
            timer = 0.5f;
        }
    }
}
