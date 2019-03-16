using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPManager : MonoBehaviour
{
    public float Hp = 20;

    private void Update()
    {
        if (Hp <= 0)
        {
            DestryObjective();
        }
        Debug.Log("VidaPlayer : " + Hp);
    }

    public void Damage(float damage)
    {
        Hp = Hp - damage;
    }
    void DestryObjective()
    {
        Destroy(gameObject);
    }
}
