using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PathFindingAStar : MonoBehaviour
{
    public Transform target;
    public Camera cam;
    public GameObject ramp;
    public NavMeshSurface surface;
    private NavMeshAgent agent;

 
    public IEnumerator WaitOneFrame()
    {
        yield return null;
        surface.BuildNavMesh();  
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                target.position = hit.point + new Vector3(0, 0.5F, 0);
                agent.SetDestination(target.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(ramp);
            StartCoroutine(WaitOneFrame());
        }
    }
}
