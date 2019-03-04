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
    
    // Start is called before the first frame update
    void Start()
    {
        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        Anim = gameObject.GetComponent<Animator>();
        Objective = GameObject.FindGameObjectWithTag("Objective");
    }

    // Update is called once per frame
    void Update()
    {
        NavAgent.SetDestination(Objective.transform.position);
        if (NavAgent.velocity.sqrMagnitude > 0)
        {
            Anim.SetBool("Moving", true);
        }else
        {
            Anim.SetBool("Moving", false);
        }

        float Distance = Vector3.Distance(transform.position, Objective.transform.position);
        if (Distance <= dToAttack)
        {
            Debug.Log("Entro");
            Anim.SetBool("Attacking", true);
            NavAgent.isStopped = true;
        }
        else
        {
            Anim.SetBool("Attacking", false);
            NavAgent.isStopped = false;
        }
    }
}
