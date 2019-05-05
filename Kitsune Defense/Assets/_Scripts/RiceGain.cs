using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiceGain : MonoBehaviour
{
    public MainObjectiveManager objectiveStatus;
    public int LevelID;
    public Image[] rice;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <3; i++)
        {
            rice[i].color = new Color(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void CheckRice()
    {
        MainData.LoadArroz();
        //Debug.Log("CheckRice");
        //Debug.Log(MainData.arrozInLevel[LevelID]);
        switch (MainData.arrozInLevel[LevelID])
        {
            case 0:
                CheckVictory();
                break;
            case 1:
                CheckObjectiveLife(15);
                break;
            case 2:
                CheckObjectiveLife(25);
                break;
        }

    }

    public void PrintArroz()
    {
        MainData.LoadArroz();
        Debug.Log(MainData.arrozInLevel[LevelID]);
    }

    public void PrintArrozTotal()
    {
        MainData.LoadArroz();
        Debug.Log(MainData.arrozTotal);
    }

    public void CheckVictory()
    {
        if(MainData.wonLevel == true)
        {
            MainData.arrozInLevel[LevelID]++;
            MainData.arrozTotal++;
            MainData.SaveArroz();
            CheckRice();
            Debug.Log("Rice Added" + MainData.arrozInLevel[LevelID]);
        }
        else
        {
            return;
        }
    }

    public void CheckObjectiveLife(float life)
    {
        if (objectiveStatus.Hp >= life)
        {
            MainData.arrozInLevel[LevelID]++;
            MainData.arrozTotal++;
            MainData.SaveArroz();
            CheckRice();
            Debug.Log("Rice Added" + MainData.arrozInLevel[LevelID]);
        } else
        {
            return;
        }
    }

    public void ShowRiceGained()
    {
        MainData.LoadArroz();
        for (int i = 0; i < MainData.arrozInLevel[LevelID]; i++)
        {
            rice[i].color = new Color(255, 255, 255);
        }
    }

    public void ResetArrozGeral()
    {
        Debug.Log("Arroz Resetado");
        MainData.ResetArroz();
    }
}
