using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPManager : MonoBehaviour
{
    public int maxHealth = 20;
    public float health = 20;
    public float healthRegeneration = 1;
    // Tempo d espera entre tomar dano e se regenerar
    public float regenWaitTime = 10;
    public bool damaged;
    //public bool canTakeDamage;

    public LevelManager levelManager;
    public Animator anim;
    //public PlayerController playerMovement;
    public GameUIManager gameUIManager;
    public Slider HPSlider;

    public bool canDie;
    private int damageIndex;
    private int deadIndex;

    private void Start()
    {
        anim = GetComponent<Animator>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        gameUIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<GameUIManager>();
        HPSlider.maxValue = maxHealth;
        canDie = true;
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

        if (health <= 0 && canDie == true)
        {
            Debug.Log("morreu");
            canDie = false;
            Death();
        }
        //Debug.Log("VidaPlayer : " + health);
    }

    public void Damage(float damage)
    {
        //anim.SetTrigger(damageIndex);
        health = health - damage;
        regenWaitTime = Time.time;
        HPSlider.value = health;
        damaged = true;
    }

    private void Death()
    {
        Debug.Log("morreu morrido");
        //playerMovement.Defeated();
        gameUIManager.Defeat();
        anim.SetTrigger(deadIndex);
        //StartCoroutine(Defeat());
    }

    public IEnumerator HealOverTime()
    {
        while (health < maxHealth && damaged == false)
        {
            health++;
            HPSlider.value = health;
            yield return new WaitForSeconds(2F);

        }
    }

   /* public IEnumerator Defeat()
    {
        float t = 0;
        while (t < 30)
        {
            t++;
            yield return new WaitForSeconds(1F);

        }
        levelManager.DelayedChangeLevel("Defeat");
    }*/

}
