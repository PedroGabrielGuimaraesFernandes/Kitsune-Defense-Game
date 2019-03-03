using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : MonoBehaviour
{
    public GameObject Objective;

    private NavMeshAgent NavAgent;

    // Start is called before the first frame update
    void Start()
    {
        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        Objective = GameObject.FindGameObjectWithTag("Objective");
    }

    // Update is called once per frame
    void Update()
    {
        NavAgent.SetDestination(Objective.transform.position);
    }
}
