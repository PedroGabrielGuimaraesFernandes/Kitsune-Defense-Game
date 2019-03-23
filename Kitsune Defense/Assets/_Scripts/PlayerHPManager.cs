using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPManager : MonoBehaviour
{
    public int maxHealth = 20;
    public float health = 20;
    public float healthRegeneration = 1;
    // Tempo d espera entre tomar dano e se regenerar
    public float regenWaitTime = 10;
    public bool damaged;
    //public bool canTakeDamage;

    public Animator anim;
    public PlayerController playerMovement;

    private int damageIndex;
    private int deadIndex;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerController>();

        damageIndex = Animator.StringToHash("damage");
        deadIndex = Animator.StringToHash("dead");
    }

    private void Update()
    {
        if (health < maxHealth && Time.time > (regenWaitTime + 10.0f) && damaged == true)
        {
            regenWaitTime = 0;
            damaged = false;
            StartCoroutine(HealOverTime());

        }

        if (health <= 0)
        {
            Death();
        }
        //Debug.Log("VidaPlayer : " + health);
    }

    public void Damage(float damage)
    {
        //anim.SetTrigger(damageIndex);
        health = health - damage;
        regenWaitTime = Time.time;
        damaged = true;
    }

    void Death()
    {
        anim.SetTrigger(deadIndex);
        
    }

    public IEnumerator HealOverTime()
    {
        while (health < maxHealth && damaged == false)
        {
            health++;
            yield return new WaitForSeconds(1F);

        }
    }

}
