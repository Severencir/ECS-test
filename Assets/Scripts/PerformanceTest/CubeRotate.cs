using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    float rotate;
    private void Start()
    {
        rotate = Random.Range(-90f, 90f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotate * Time.deltaTime, 0);
    }
}
