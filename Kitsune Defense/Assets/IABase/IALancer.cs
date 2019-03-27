using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IALancer : IABase
{
    enum States { Idle, GoObjective, Battle };
    States ActualState;

    // Start is called before the first frame update
    void Start()
    {
        InicialSetup();
        ActualState = States.GoObjective;
    }

    // Update is called once per frame
    void Update()
    {
        switch (ActualState)
        {
            case States.Idle:
                Idle();
                break;
            case States.GoObjective:
                GoObjective();
                break;
            case States.Battle:
                Batlle();
                break;
        }
        CheckForPlayer(30);
        if (Objective != null)
        {
            float Distance = Vector3.Distance(transform.position, Objective.transform.position);
            if (NavAgent.velocity.sqrMagnitude < 0)
            {
                ActualState = States.Idle;
            }
            else if (Distance <= dToAttack)
            {
                ActualState = States.Battle;
            }
            else
            {
                ActualState = States.GoObjective;
            }
        }
        else
        {
            ActualState = States.Idle;
        }
        if (hp <= 0)
        {
            Death();
        }
        if (Input.GetKey(KeyCode.K))
        {
            hp = 0;
        }
        
    }
    public void DealDamage()
    {
        if (Objective == MainObjective)
        {
            ObjctiveManagerScript.Damage(Damage);
        }
        else
        {
            PlayerManagerScript.Damage(Damage);
        }
    }
}
