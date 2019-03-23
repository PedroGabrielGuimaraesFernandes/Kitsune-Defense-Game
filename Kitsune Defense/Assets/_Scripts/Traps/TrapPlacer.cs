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

    public RaycastHit hitInfo;
    public RaycastHit[] lateralHitInfo;
    public Ray[] rayDirections;
    public Vector3[] directions;
    public int playerMask = 1;


    private bool selectedTrapHorizontal;
    private bool selectedTrapVertical;
    private int usingTrap = -1;
    private GameObject previewTrap;
    // Start is called before the first frame update
    void Start()
    {
        lateralHitInfo = new RaycastHit[4];
        playerMask = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        Debug.DrawRay(ray.origin + Vector3.up, ray.direction * 10, Color.green);
        Debug.DrawRay(ray.origin + Vector3.down, ray.direction * 10, Color.gray);
        Debug.DrawRay(ray.origin+Vector3.right, ray.direction * 10, Color.red);
        Debug.DrawRay(ray.origin + Vector3.left, ray.direction * 10, Color.blue);*/
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
        
        //RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //rayDirections = new Ray[] { (ray.origin,),}
        //directions = new Vector3[] { Vector3.up, Vector3.forward, Vector3.back, Vector3.down};
        Color[] colors = new Color[] { Color.yellow, Color.red, Color.blue, Color.green};

        if (Physics.Raycast(ray, out hitInfo, 10) && (hitInfo.collider.CompareTag("Ground") || hitInfo.collider.CompareTag("Wall")))
        {
            Debug.DrawRay(ray.origin,ray.direction,Color.black);
            Debug.Log(hitInfo.normal);
            var finalposition = grid.GetNearestPointOnGrid(hitInfo.point);
            if (hitInfo.normal.y != 0)
            {
                directions = new Vector3[] {finalposition + Vector3.right + hitInfo.normal, finalposition + hitInfo.normal + Vector3.right, finalposition + hitInfo.normal + Vector3.left, finalposition + hitInfo.normal + Vector3.back };
            }
            else if (hitInfo.normal.x != 0)
            {
                directions = new Vector3[] { finalposition + Vector3.forward + hitInfo.normal, finalposition + hitInfo.normal + Vector3.up, finalposition + hitInfo.normal + Vector3.down, finalposition + hitInfo.normal + Vector3.back };
            }
            else
            {
                directions = new Vector3[] { finalposition + Vector3.up + hitInfo.normal, finalposition + hitInfo.normal + Vector3.right, finalposition + hitInfo.normal + Vector3.left, finalposition + hitInfo.normal + Vector3.down };
            }
            int SameTag = 0;
            string usingtag = hitInfo.collider.tag;

            for (int i = 0; i < lateralHitInfo.Length; i++)
            {
                if (Physics.Raycast(directions[i],-hitInfo.normal, out lateralHitInfo[i], 15) && (lateralHitInfo[i].collider.CompareTag(usingtag)))
                {
                    Debug.DrawRay( directions[i], -hitInfo.normal, colors[i]);
                    SameTag++;
                    Debug.Log("Raycast " + i + " Mesma tag e achou algo" + lateralHitInfo[i].collider.tag);
                }
                else if (Physics.Raycast( directions[i], -hitInfo.normal, out lateralHitInfo[i], 15))
                {
                    Debug.DrawRay( directions[i], -hitInfo.normal, colors[i]);
                    Debug.Log("Raycast " + i + " achou algo mas a tag é diferente " + lateralHitInfo[i].collider.tag);
                } else
                {
                    Debug.DrawRay( directions[i], -hitInfo.normal, colors[i]);
                    Debug.Log("Raycast " + i + " achou nada");
                    return;
                }
            }

            if(SameTag>=4){
                //var finalposition = grid.GetNearestPointOnGrid(hitInfo.point);
                //checagem de outras traps na area
                Collider[] hitColliders = Physics.OverlapSphere(finalposition, grid.size / 2);

                for (int t = 0; t < hitColliders.Length; t++)
                {
                    if (hitColliders[t].tag == "Trap")
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
                        } else if (hitInfo.normal.x != 0)
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

    private void OnDrawGizmos()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitUpInfo;

        RaycastHit hitDownInfo;

        RaycastHit hitRightInfo;

        RaycastHit hitLeftInfo;

        Gizmos.color = Color.red;
        //Gizmos.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.ScreenToWorldPoint(Input.mousePosition)+ new Vector3(0,0,10));

        Gizmos.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        Gizmos.color = Color.black;

        Gizmos.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.down * 10), Vector3.forward);

        Gizmos.color = Color.yellow;

        Gizmos.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.up * 10), Vector3.forward);

        Gizmos.color = Color.blue;

        Gizmos.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.right * 10), Vector3.forward);

        Gizmos.color = Color.green;

        Gizmos.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.left * 10), Vector3.forward);


    }

}
