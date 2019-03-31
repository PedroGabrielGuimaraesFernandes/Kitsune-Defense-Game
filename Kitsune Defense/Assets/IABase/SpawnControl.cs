﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public GameObject LancerEnemy;
    public GameObject ArcherEnemy;
    public GameObject[] SpawnHootsList;
    GameObject SpawnHoot;
    float Time;
    int MaxWavesNumber = 3;
    static public int KilledEnemies;
    int CurrentWave = 1;
    int MaxEnemysNumber;
    int CurrentEnemyNumber;
    int SpawIndex = 0;

    public GameUIManager gameUIManager;

    void Start()
    {
        SpawnHoot = SpawnHootsList[0];
        gameUIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<GameUIManager>();
        KilledEnemies = 0;
        StartCoroutine(SpawnCorroutine());
    }
    private void Update()
    {
        switch (CurrentWave)
        {
            case 1:
                MaxEnemysNumber = 2;
                Time = 5;
                SpawnHoot = SpawnHootsList[0];
                break;
            case 2:
                MaxEnemysNumber = 5;
                Time = 3;
                SpawnHoot = SpawnHootsList[1];
                break;
            case 3:
                MaxEnemysNumber = 10;
                Time = 2;
                SpawnHoot = SpawnHootsList[0];
                break;
        }
        if (KilledEnemies == MaxEnemysNumber)
        {
            CurrentWave++;
            SpawIndex = 0;
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
    public IEnumerator SpawnCorroutine()
    {
        GameObject Enemy;
        if (SpawIndex <=2)
        {
            Enemy = Instantiate(LancerEnemy, SpawnHoot.transform.position, SpawnHoot.transform.rotation) as GameObject;
            SpawIndex ++;
        }
        else if (SpawIndex <=4)
        {
            Enemy = Instantiate(ArcherEnemy, SpawnHoot.transform.position, SpawnHoot.transform.rotation) as GameObject;
            SpawIndex ++;
        }
        else if (SpawIndex == 5)
        {
            Enemy = Instantiate(ArcherEnemy, SpawnHoot.transform.position, SpawnHoot.transform.rotation) as GameObject;
            SpawIndex = 0;
        }
        CurrentEnemyNumber++;
        yield return new WaitForSeconds(Time);
        if (CurrentEnemyNumber < MaxEnemysNumber)
        {
            StartCoroutine(SpawnCorroutine());
        }
    }
    public void Victory()
    {
        gameUIManager.Victory();
    }
}
