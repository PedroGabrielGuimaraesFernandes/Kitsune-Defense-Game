using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : MonoBehaviour
{
    public float dToAttack;
    public float hp ;
    public float Damage;

    [HideInInspector] public GameObject Objective;
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public Animator Anim;
    [HideInInspector] public GameObject MainObjective;
    [HideInInspector] public GameObject PlayerObj;
    [HideInInspector] public PlayerHPManager PlayerManagerScript;
    [HideInInspector] public MainObjectiveManager ObjctiveManagerScript;

    // Start is called before the first frame update
    public void InicialSetup()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        MainObjective = GameObject.FindGameObjectWithTag("Objective");

        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        Anim = gameObject.GetComponent<Animator>();
        PlayerManagerScript = PlayerObj.GetComponent<PlayerHPManager>();
        ObjctiveManagerScript = MainObjective.GetComponent<MainObjectiveManager>();

        Objective = MainObjective;   
    }
    public void Idle()
    {
        Anim.SetBool("Moving", false);
        Anim.SetBool("Attacking", false);
        NavAgent.isStopped = true;
    }
    public void GoObjective()
    {
        NavAgent.SetDestination(Objective.transform.position);
        Anim.SetBool("Moving", true);
        Anim.SetBool("Attacking", false);
        NavAgent.isStopped = false;
    }
    public void Batlle()
    {
        Anim.SetBool("Moving", false);
        Anim.SetBool("Attacking", true);
        NavAgent.isStopped = true;
        transform.LookAt(Objective.transform);
        gameObject.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }
    public void CheckForPlayer(float RangeDetection)
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
    public void Death()
    {
        SpawnControl.KilledEnemies++;
        Destroy(gameObject);
        return;
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
    }
}
