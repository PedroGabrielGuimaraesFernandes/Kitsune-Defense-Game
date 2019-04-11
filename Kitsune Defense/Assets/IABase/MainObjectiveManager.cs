using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjectiveManager : MonoBehaviour
{

    public float Hp = 100;
    public LevelManager levelManager;
    public GameUIManager gameUIManager;

    private bool defeated;

    private void Start()
    {
        Hp = 30;
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        gameUIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<GameUIManager>();
    }

    private void Update()
    {
        //Debug.Log("VidaCastelo : " + Hp);
        if (Hp <= 0 && defeated == false)
        {
            Defeat();
        }
    }

    public void Damage(float damage)
    {
        Hp = Hp-damage;
    }

    public void Defeat()
    {
        defeated = true;
        gameUIManager.Defeat();
    }

    /*void DestryObjective()
    {
        Destroy(gameObject);
    }*/
}
