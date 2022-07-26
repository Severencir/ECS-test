using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public static int count;
    public GameObject cube;
    bool spawn = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawn = !spawn;
        }
        if (spawn)
        {
            Instantiate(cube, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), Quaternion.identity);
            count++;
        }
    }
}
