using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : MonoBehaviour
{
    public GameObject Objective;
    public float dToAttack;

    private NavMeshAgent NavAgent;
    private Animator Anim;

    enum States {Idle,GoObjective,Battle};
    States ActualState;

    // Start is called before the first frame update
    void Start()
    {
        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        Anim = gameObject.GetComponent<Animator>();
        Objective = GameObject.FindGameObjectWithTag("Objective");
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
}
