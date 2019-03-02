using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainData
{

    public static string NextScene;
    public static float musicVolume;
    public static float sfxVolume;
    public static int[] levelStatus = { 1, 0, 0, 0, 0, 0, 0, 0 };



    public static void SaveData()
    {
        PlayerPrefs.SetFloat("MVolume", musicVolume);
        PlayerPrefs.SetFloat("MVolume", sfxVolume);
        PlayerPrefsX.SetIntArray("levelStatus", levelStatus);
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
            levelStatus = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };
        }
    }

    public static void ResetLevels()
    {
        levelStatus = new int[] { 1, 0, 0, 0, 0, 0, 0, 0 };
    }
}

