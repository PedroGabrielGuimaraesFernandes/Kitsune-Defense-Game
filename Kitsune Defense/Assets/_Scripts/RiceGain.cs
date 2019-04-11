using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceGain : MonoBehaviour
{
    public MainObjectiveManager objectiveStatus;
    public int LevelID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void CheckRice()
    {
        Debug.Log("CheckRice");
        Debug.Log(MainData.arrozInLevel[LevelID]);
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
        Debug.Log(MainData.arrozInLevel[LevelID]);
    }

    public void CheckVictory()
    {
        if(MainData.wonLevel == true)
        {
            MainData.arrozInLevel[LevelID]++;
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
            CheckRice();
            Debug.Log("Rice Added" + MainData.arrozInLevel[LevelID]);
        } else
        {
            return;
        }
    }
    public void ResetArrozGeral()
    {
        MainData.ResetArroz();
    }
}
