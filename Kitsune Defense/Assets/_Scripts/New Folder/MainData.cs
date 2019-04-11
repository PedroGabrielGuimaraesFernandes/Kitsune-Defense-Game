using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainData
{

    public static string NextScene;
    public static float musicVolume;
    public static float sfxVolume;
    public static bool placingTraps;
    public static bool waitingNextWave;
    public static bool wonLevel;
    //variavel do total de arroz
    public static int arrozTotal;
    public static int[] levelStatus = { 1, 0, 0, 0, 0};
    //0 - SpikeTrap, 1 - ProjectileTrap, 2 - SlowTrap.
    public static int[] canUseTrap = { 1, 1, 0 };
    //Array para guardar o arroz coletado por fase
    public static int[] arrozInLevel = { 0, 0, 0, 0, 0};



    public static void SaveData()
    {
        PlayerPrefs.SetFloat("MVolume", musicVolume);
        PlayerPrefs.SetFloat("MVolume", sfxVolume);
        PlayerPrefsX.SetIntArray("levelStatus", levelStatus);
        PlayerPrefsX.SetIntArray("canUseTrap", canUseTrap);
        PlayerPrefsX.SetIntArray("arrozInLevel", arrozInLevel);
        Debug.Log(MainData.levelStatus[1]);
    }

    public static void LoadData()
    {
        musicVolume = PlayerPrefs.GetFloat("MVolume", 10);
        sfxVolume = PlayerPrefs.GetFloat("MVolume", 10);
        if (PlayerPrefs.HasKey("levelStatus"))
        {
            levelStatus = PlayerPrefsX.GetIntArray("levelStatus");
        }
        else
        {
            levelStatus = new int[] { 1, 0, 0, 0, 0};
        }

        if (PlayerPrefs.HasKey("canUseTrap"))
        {
            canUseTrap = PlayerPrefsX.GetIntArray("canUseTrap");
        }
        else
        {
            canUseTrap = new int[] { 1, 1, 0};
        }

        if (PlayerPrefs.HasKey("arrozInLevel"))
        {
            arrozInLevel = PlayerPrefsX.GetIntArray("arrozInLevel");
        }
        else
        {
            arrozInLevel = new int[] { 0, 0, 0, 0, 0 };
        }
    }

    public static void ResetLevels()
    {
        levelStatus = new int[] { 0, 0, 0, 0, 0};
    }


    public static void ResetTraps()
    {
        canUseTrap = new int[] { 1, 1, 0};
    }

    public static void ResetArroz()
    {
        arrozInLevel = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };
    }
}

