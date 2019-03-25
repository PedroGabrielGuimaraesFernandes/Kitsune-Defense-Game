using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public GameObject LancerEnemy;
    public GameObject[] SpawnHoots;
    float Time;
    int MaxWavesNumber = 3;
    static public int KilledEnemies;
    int CurrentWave = 1;
    int MaxEnemysNumber;
    int CurrentEnemyNumber;

    void Start()
    {
        StartCoroutine(SpawnCorroutine());
    }
    private void Update()
    {
        switch (CurrentWave)
        {
            case 1:
                MaxEnemysNumber = 2;
                Time = 5;
                break;
            case 2:
                MaxEnemysNumber = 5;
                Time = 3;
                break;
            case 3:
                MaxEnemysNumber = 10;
                Time = 2;
                break;
        }
        if (KilledEnemies == MaxEnemysNumber)
        {
            CurrentWave++;
            KilledEnemies = 0;
            CurrentEnemyNumber = 0;
            if (CurrentWave <= MaxWavesNumber)
            {
                StartCoroutine(SpawnCorroutine());
            }
            else
            {
                Victory();
            }
        }     
    }
    IEnumerator SpawnCorroutine()
    {
        Instantiate(LancerEnemy, SpawnHoots[0].transform);
        CurrentEnemyNumber++;
        yield return new WaitForSeconds(Time);
        if (CurrentEnemyNumber < MaxEnemysNumber)
        {
            StartCoroutine(SpawnCorroutine());
        }
    }
    public void Victory()
    {
        MyLoad.Loading("Victory");
    }
}
