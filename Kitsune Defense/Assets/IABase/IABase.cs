using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : MonoBehaviour
{
    public float hp = 10;

    public GameObject Objective;
    public float dToAttack;

    private NavMeshAgent NavAgent;
    private Animator Anim;
    private GameObject MainObjective;
    private GameObject PlayerObj;

    PlayerHPManager PlayerManagerScript;
    MainObjectiveManager ObjctiveManagerScript;

    enum States {Idle,GoObjective,Battle};
    States ActualState;

    // Start is called before the first frame update
    void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        MainObjective = GameObject.FindGameObjectWithTag("Objective");

        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        Anim = gameObject.GetComponent<Animator>();
        PlayerManagerScript = PlayerObj.GetComponent<PlayerHPManager>();
        ObjctiveManagerScript = MainObjective.GetComponent<MainObjectiveManager>();

        Objective = MainObjective;
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
    }
    void Idle()
    {
        Anim.SetBool("Moving", false);
        Anim.SetBool("Attacking", false);
        NavAgent.isStopped = true;
    }
    void GoObjective()
    {
        NavAgent.SetDestination(Objective.transform.position);
        Anim.SetBool("Moving", true);
        Anim.SetBool("Attacking", false);
        NavAgent.isStopped = false;
    }
    void Batlle()
    {
        Anim.SetBool("Moving", false);
        Anim.SetBool("Attacking", true);
        NavAgent.isStopped = true;
    }
    public void DealDamage(float damage)
    {
        if (Objective == MainObjective)
        {
            ObjctiveManagerScript.Damage(damage);
        }
        else
        {
            PlayerManagerScript.Damage(damage);
        }
    }
    void CheckForPlayer(float RangeDetection)
    {
       float Distance = Vector3.Distance(transform.position, PlayerObj.transform.position);
       if (Distance <= RangeDetection)
       {
            Objective = PlayerObj;
       }
        else
        {
            Objective = MainObjective;
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

}
