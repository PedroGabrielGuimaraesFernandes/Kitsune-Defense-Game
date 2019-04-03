using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : MonoBehaviour
{
    public float dToAttack;
    public float hp ;
    public float Damage;
    public float WaitToAttack;
    public GameObject redSpirit;


    [HideInInspector] public GameObject Objective;
    [HideInInspector] public NavMeshAgent NavAgent;
    [HideInInspector] public Animator Anim;
    [HideInInspector] public GameObject MainObjective;
    [HideInInspector] public GameObject PlayerObj;
    [HideInInspector] public PlayerHPManager PlayerManagerScript;
    [HideInInspector] public MainObjectiveManager ObjctiveManagerScript;
    [HideInInspector] public bool AtkBool;

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
        NavAgent.isStopped = true;
    }
    public void GoObjective()
    {
        NavAgent.SetDestination(Objective.transform.position);
        Anim.SetBool("Moving", true);
        NavAgent.isStopped = false;
    }
    public IEnumerator AttackCorroutine()
    {
        if (!AtkBool)
        {
            AtkBool = true;
            Anim.SetBool("Moving", false);
            Anim.SetTrigger("Attack");
            NavAgent.isStopped = true; ;
            Vector3.Lerp(gameObject.transform.eulerAngles, new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z), 1);
            yield return new WaitForSeconds(WaitToAttack);
            AtkBool = false;
        }
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
        GameObject orb = Instantiate(redSpirit, transform.position + Vector3.up, Quaternion.identity);
        Destroy(gameObject);
        return;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }

    public void ReduceSpeed()
    {
        NavAgent.speed = 0.5f;
    }


    public void SpeedBackToNormal()
    {
        NavAgent.speed = 3.5f;
    }
}
