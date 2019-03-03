using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public GameObject MassSpawn;
    public float Time;
    
    void Start()
    {
        StartCoroutine(SpawnCorroutine());
    }

    IEnumerator SpawnCorroutine()
    {
        Instantiate(MassSpawn, transform);
        yield return new WaitForSeconds(Time);
        StartCoroutine(SpawnCorroutine());
    }
}
