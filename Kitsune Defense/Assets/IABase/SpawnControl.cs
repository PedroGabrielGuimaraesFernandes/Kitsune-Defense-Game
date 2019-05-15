using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnControl : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] SpawnPointList;
    public Wave[] waves;//
    public int WaveChangeTime;
    //GameObject SpawnPoint;
    float Time;
    //int MaxWavesNumber = 3;
    //Minhas variaveis 
    int lancerNumber;
    int archerNumber;
    int samuraiNumber;
    bool waveOngoing;
    public bool spawningWave;
    //
    static public int KilledEnemies;
    static public int AllKilledEnemies;
    //static public int AllPassEnemies;
    static public int AllEnemiesSpawned;
    public int CurrentWave = 0;
    [SerializeField]
    public int MaxEnemiesNumber;
    [SerializeField]
    public static int CurrentEnemyNumber;
    int SpawIndex = 0;

    public GameUIManager gameUIManager;

    void Start()
    {
        gameUIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<GameUIManager>();
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].totalQuantity = waves[i].LancerQuantity + waves[i].ArcherQuantity + waves[i].SamuraiQuantity;
            MaxEnemiesNumber += waves[i].totalQuantity;
        }
        KilledEnemies = 0;
        CurrentEnemyNumber = 0;
        MainData.waitingNextWave = true;
    }
    private void Update()
    {
        //Debug.Log(AllKilledEnemies);
        //Debug.Log(AllPassEnemies);
        switch (CurrentWave)
        {
            case 1:
                
                Time = 2;
                //SpawnPoint = SpawnPointList[0];
                AllEnemiesSpawned = AllEnemiesSpawned + MaxEnemiesNumber;
                break;
            case 2:
                Time = 2;
                //SpawnPoint = SpawnPointList[1];
                AllEnemiesSpawned = AllEnemiesSpawned + MaxEnemiesNumber;
                break;
            case 3:
                Time = 2;
                //SpawnPoint = SpawnPointList[0];
                AllEnemiesSpawned = AllEnemiesSpawned + MaxEnemiesNumber;
                break;
        }
        if (CurrentEnemyNumber <= 0 && spawningWave == false) //(KilledEnemies == MaxEnemiesNumber)
        {
            CurrentWave++;
            spawningWave = true;
            MainData.waitingNextWave = true;
            Debug.Log("WaitingWave");
            //SpawIndex = 0;
            
            if (CurrentWave <= waves.Length)
            {
                if (CurrentWave != 1) {
                    StartCoroutine(CallSpawnCorroutine(WaveChangeTime));
                }
            }
            else 
            {
                Victory();
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && CurrentWave == 1 && MainData.waitingNextWave)
        {
            Debug.Log("First wave");
            gameUIManager.WavesBegan();
            StartCoroutine(SpawnCorroutine(0));
        }
    }
    public IEnumerator SpawnCorroutine(int onda)
    {
        GameObject Enemy;
        while (waves[onda].totalQuantity > CurrentEnemyNumber)
        {
            if (waves[onda].useSpawnPoint1)
            {
                if (waves[onda].LancerQuantity > lancerNumber)
                {
                    Enemy = Instantiate(Enemies[0], SpawnPointList[0].transform.position, SpawnPointList[0].transform.rotation) as GameObject;
                    CurrentEnemyNumber++;
                    lancerNumber++;
                }

                if (waves[onda].ArcherQuantity > archerNumber)
                {
                    Enemy = Instantiate(Enemies[1], SpawnPointList[0].transform.position, SpawnPointList[0].transform.rotation) as GameObject;
                    CurrentEnemyNumber++;
                    archerNumber++;
                }

                if (waves[onda].SamuraiQuantity > samuraiNumber)
                {
                    Enemy = Instantiate(Enemies[2], SpawnPointList[0].transform.position, SpawnPointList[0].transform.rotation) as GameObject;
                    CurrentEnemyNumber++;
                    samuraiNumber++;
                }
            }

            if (waves[onda].useSpawnPoint2)
            {
                if (waves[onda].LancerQuantity > lancerNumber)
                {
                    Enemy = Instantiate(Enemies[0], SpawnPointList[1].transform.position, SpawnPointList[0].transform.rotation) as GameObject;
                    CurrentEnemyNumber++;
                    lancerNumber++;
                }

                if (waves[onda].ArcherQuantity > archerNumber)
                {
                    Enemy = Instantiate(Enemies[1], SpawnPointList[1].transform.position, SpawnPointList[0].transform.rotation) as GameObject;
                    CurrentEnemyNumber++;
                    archerNumber++;
                }

                if (waves[onda].SamuraiQuantity > samuraiNumber)
                {
                    Enemy = Instantiate(Enemies[2], SpawnPointList[1].transform.position, SpawnPointList[0].transform.rotation) as GameObject;
                    CurrentEnemyNumber++;
                    samuraiNumber++;
                }
            }
                yield return new WaitForSeconds(Time);
        }
        MainData.waitingNextWave = false;
        spawningWave = false;
        lancerNumber = 0;
        archerNumber = 0;
        samuraiNumber = 0;
        Debug.Log("WaveBegan");
        /*if (CurrentEnemyNumber < MaxEnemiesNumber)
        {
            StartCoroutine(SpawnCorroutine());
        }*/
    }

    public IEnumerator CallSpawnCorroutine(int time)
    {
        yield return new WaitForSeconds(time); 

        SpawnCall();
    }
        public void SpawnCall()
    {
        gameUIManager.WavesBegan();
        StartCoroutine(SpawnCorroutine(CurrentWave -1));
    }  
    public void Victory()
    {
        gameUIManager.Victory();
    }
}
