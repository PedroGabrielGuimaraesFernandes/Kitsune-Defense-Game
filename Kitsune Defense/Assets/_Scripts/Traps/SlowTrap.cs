using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : Trap
{
    // Start is called before the first frame update
    void Start()
    {
        /*if (MainData.upgrades[trapID] > 0)
        {
            Debug.Log("Tento eu Acho");
            cost = cost - (5 * MainData.upgrades[trapID]);
        }*/

        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IABase enemyScript = other.gameObject.GetComponent<IABase>();
            enemyScript.ReduceSpeed();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IABase enemyScript = other.gameObject.GetComponent<IABase>();
            enemyScript.SpeedBackToNormal();
        }
    }
}
