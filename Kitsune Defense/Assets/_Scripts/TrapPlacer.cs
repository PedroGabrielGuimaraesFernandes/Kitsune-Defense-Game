using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlacer : MonoBehaviour
{

    [Header("Local onde vai o grid da fase")]
    //Talvez fazer uma array para poder por grids d tamanhos diferentes
    public Grid grid;
    public GameObject Trap;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hitInfo)/* && hitInfo.collider.CompareTag("Chão&Parede")*/)
            {
                PlaceTrapNear(hitInfo.point);
            }
        }
    }

    public void PlaceTrapNear(Vector3 clickPoint)
    {
        var finalposition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject armadilha = Instantiate(Trap,finalposition, Quaternion.identity);

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = clickPoint;
    }
}
