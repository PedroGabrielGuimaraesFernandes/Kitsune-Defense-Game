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
    //Array para guardar os upgrades
    public static int[] upgrades = { 0, 0, 0};
    // Array com as descrições das traps
    public static string[] trapDescription = {"Espinhos de bambu que saem do chão para causar dano aos inimigos", "Lança kunais que causam dano nos inimigos nos inimigos(causa dano por projetil)", "Inimigos se movem mais lentamente enquanto pisam na trap"  };


    //funções principais

    public static void SaveData()
    {
        PlayerPrefs.SetFloat("MVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetInt("arrozTotal", arrozTotal);
        PlayerPrefsX.SetIntArray("levelStatus", levelStatus);

        PlayerPrefsX.SetIntArray("canUseTrap", canUseTrap);
        PlayerPrefsX.SetIntArray("arrozInLevel", arrozInLevel);
        PlayerPrefsX.SetIntArray("upgrades", upgrades);
    }

    public static void LoadData()
    {
        musicVolume = PlayerPrefs.GetFloat("MVolume", 10);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 10);
        arrozTotal = PlayerPrefs.GetInt("arrozTotal", 0);
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

        if (PlayerPrefs.HasKey("upgrades"))
        {
            upgrades = PlayerPrefsX.GetIntArray("upgrades");
        }
        else
        {
            upgrades = new int[] { 0, 0, 0 };
        }
    }

    // Saves

    public static void SaveArroz()
    {
        PlayerPrefs.SetInt("arrozTotal", arrozTotal);
        PlayerPrefsX.SetIntArray("arrozInLevel", arrozInLevel);
    }

    // Load

    public static void LoadArroz()
    {
        arrozTotal = PlayerPrefs.GetInt("arrozTotal", 0);
        if (PlayerPrefs.HasKey("arrozInLevel"))
        {
            arrozInLevel = PlayerPrefsX.GetIntArray("arrozInLevel");
        }
        else
        {
            arrozInLevel = new int[] { 0, 0, 0, 0, 0 };
        }
    }

    // Resets

    public static void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public static void ResetLevels()
    {
        //levelStatus = new int[] { 0, 0, 0, 0, 0};
        PlayerPrefs.DeleteKey("levelStatus");
    }


    public static void ResetTraps()
    {
        //canUseTrap = new int[] { 1, 1, 0};
        PlayerPrefs.DeleteKey("canUseTrap");
        PlayerPrefs.DeleteKey("upgrades");
    }

    public static void ResetArroz()
    {
        //arrozInLevel = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };
        PlayerPrefs.DeleteKey("arrozTotal");
        PlayerPrefs.DeleteKey("arrozInLevel");
    }
}

