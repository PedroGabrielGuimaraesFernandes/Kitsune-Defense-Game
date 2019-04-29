using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    
    public Transform reference;
    public Collider detector;
    public GameObject readySpike;
    public Collider attacker;
    public GameObject Spikes;
    public bool attacking;

    private int readyIndex;
    private int attackIndex;
    private int withdrawIndex;
    private int reloadIndex;

    // Start is called before the first frame update
    void Start()
    {
        /*if (MainData.upgrades[trapID] > 0)
        {
            damage = damage + (5 * MainData.upgrades[trapID]);
        }*/
        anim = GetComponent<Animator>();
        readyIndex = Animator.StringToHash("ready");
        attackIndex = Animator.StringToHash("attack");
        withdrawIndex = Animator.StringToHash("withdraw");
        reloadIndex = Animator.StringToHash("reload");
    }

    public void WithdrawSpikes()
    {
        anim.SetTrigger(withdrawIndex);
    }

    public void attack()
    {
        attacking = true;
        attacker.enabled = true;
        //Spikes.SetActive(true);
        //StartCoroutine((WithdrawSpikes(2)));

    }


    public void ReloadTrap(float tempo)
    {
        anim.SetTrigger(reloadIndex);
        attacker.enabled = false;
        reloading = true;
        attacking = false;
        StartCoroutine(ReloadTimer(tempo));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && attacking == false)
        {
            //Debug.Log("Detected Enemy");
            detector.enabled = false;
            anim.SetTrigger(attackIndex);
        } else if (other.gameObject.CompareTag("Enemy") && attacking == true)
        {
            //Debug.Log("Enemy Hurt");
            IABase enemyScript = other.gameObject.GetComponent<IABase>();
            enemyScript.TakeDamage(damage);
        }
    }


    /*public IEnumerator WithdrawSpikes(float timeForWithdraw)
    {
        float t = 0;
        while (t < timeForWithdraw)
        {
            t++;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("withdraw");
        
        
    }*/

    public IEnumerator ReloadTimer(float time)
    {
        float r = 0;
        //vai se repetir a te dar o tempo do reload
        while (r < reloadTime)
        {
            r++;
            yield return new WaitForSeconds(1f);
        }
        detector.enabled = true;
        reloading = false;
        anim.SetTrigger(readyIndex);
    }
}
