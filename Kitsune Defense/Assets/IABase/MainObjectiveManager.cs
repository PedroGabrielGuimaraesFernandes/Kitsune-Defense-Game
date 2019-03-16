using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjectiveManager : MonoBehaviour
{

    public float Hp = 100;

    private void Update()
    {
        Debug.Log("VidaCastelo : " + Hp);
        if (Hp <= 0)
        {
            DestryObjective();
        }
    }

    public void Damage(float damage)
    {
        Hp = Hp-damage;
    }
    void DestryObjective()
    {
        Destroy(gameObject);
    }
}
