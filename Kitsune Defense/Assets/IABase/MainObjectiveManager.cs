using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjectiveManager : MonoBehaviour
{

    public float Hp = 100;
    public LevelManager levelManager;
    public PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        Debug.Log("VidaCastelo : " + Hp);
        if (Hp <= 0)
        {
            //DestryObjective();
            Defeat();
        }
    }

    public void Damage(float damage)
    {
        Hp = Hp-damage;
    }

    public void Defeat()
    {
        player.Defeated();
    }

    /*void DestryObjective()
    {
        Destroy(gameObject);
    }*/
}
