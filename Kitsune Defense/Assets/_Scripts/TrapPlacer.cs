using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlacer : MonoBehaviour
{

    [Header("Local onde vai o grid da fase")]
    //Talvez fazer uma array para poder por grids d tamanhos diferentes
    public Grid grid;
    public Trap[] traps;
    [Range (0,10)]
    public int selectedTrap;

    public GameObject previewTrapObject;
    [Range (0,4)]
    public float trapsRotation;

    private bool selectedTrapHorizontal;
    private bool selectedTrapVertical;
    private int usingTrap = -1;
    private GameObject previewTrap;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Input.GetAxisRaw("Mouse ScrollWheel"));
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && selectedTrap < traps.Length-1)
        {
            selectedTrap += 1;
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel")< 0 && selectedTrap > 0)
        {
            selectedTrap -= 1;
        }

        if (usingTrap == selectedTrap)
        {
            PlacePreview();
        } else
        {
            usingTrap = selectedTrap;
            selectedTrapHorizontal = traps[selectedTrap].horizontal;
            selectedTrapVertical = traps[selectedTrap].vertical;
            Destroy(previewTrap);
            return;
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (trapsRotation < 4)
            {
                trapsRotation += 1;
            }
            else
            {
                trapsRotation = 0;
            }
        }
    }

    public void PlacePreview()
    {
        
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        if (Physics.Raycast(ray, out hitInfo, 10) && (hitInfo.collider.CompareTag("Ground") || hitInfo.collider.CompareTag("Wall")))
        {

            var finalposition = grid.GetNearestPointOnGrid(hitInfo.point);
            //checagem de outras traps na area
            Collider[] hitColliders = Physics.OverlapSphere(finalposition, grid.size / 2);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "Trap")
                {
                    if (previewTrap != null)
                    {
                        Destroy(previewTrap);
                        return;
                    }
                    return;
                }
            }
            Debug.Log(hitInfo.normal);
            Debug.Log(hitInfo.collider.tag);

            //instancia o preview ou muda a sua posição
            if (previewTrap == null)
            {
                //previewTrap = Instantiate(previewTrapObject, hitInfo.point, Quaternion.identity);

                if (selectedTrapHorizontal == true && hitInfo.collider.CompareTag("Ground"))
                {
                    previewTrap = Instantiate(previewTrapObject, finalposition, Quaternion.identity);

                    previewTrap.transform.localScale = traps[selectedTrap].horDimentions;
                }
                else if (selectedTrapVertical == true && hitInfo.collider.CompareTag("Wall"))
                {
                    previewTrap = Instantiate(previewTrapObject, finalposition, Quaternion.identity);
                    previewTrap.transform.localScale = traps[selectedTrap].vertDimentions;
                    previewTrap.transform.up = hitInfo.normal /*+ new Vector3(0, 90, 0) * trapsRotation */;
                }
                else
                {
                    return;
                }
            }
            else
            {
                //previewTrap.transform.position = finalposition;
                if (selectedTrapHorizontal == true && hitInfo.collider.CompareTag("Ground"))
                {
                    previewTrap.transform.position = finalposition;
                    if (hitInfo.normal.y != 0)
                    {
                        previewTrap.transform.up = hitInfo.normal;
                        previewTrap.transform.rotation = Quaternion.Euler(previewTrap.transform.rotation.eulerAngles + new Vector3(0, 90, 0) * trapsRotation);
                    }
                }
                else if (selectedTrapVertical == true && hitInfo.collider.CompareTag("Wall"))
                {
                    previewTrap.transform.position = finalposition;
                    if (hitInfo.normal.y != 0)
                    {
                        previewTrap.transform.up = hitInfo.normal;
                        previewTrap.transform.rotation = Quaternion.Euler(previewTrap.transform.rotation.eulerAngles + new Vector3(0, 90, 0) * trapsRotation);
                    }else if (hitInfo.normal.x != 0)
                    {

                        previewTrap.transform.up = hitInfo.normal;
                        previewTrap.transform.rotation = Quaternion.Euler(previewTrap.transform.rotation.eulerAngles + new Vector3(90, 0, 0) * trapsRotation);
                    }
                    else if (hitInfo.normal.z != 0)
                    {
                        previewTrap.transform.up = hitInfo.normal;
                        previewTrap.transform.rotation = Quaternion.Euler(previewTrap.transform.rotation.eulerAngles + new Vector3(-90 * trapsRotation, 90, 90));
                    }

                }
            }
        }
        else if (previewTrap != null)
        {
            usingTrap = selectedTrap;
            Destroy(previewTrap);
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            //RaycastHit hitInfo;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo) && previewTrap != null)
            {
                PlaceTrapNear(hitInfo.point);
            }
        }
    }



    public void PlaceTrapNear(Vector3 clickPoint)
    {
        var finalposition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject armadilha = Instantiate(traps[selectedTrap].gameObject,previewTrap.transform.position, previewTrap.transform.rotation);

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = clickPoint;
    }
}
