               using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapPlacer : MonoBehaviour
{
    public bool isPlacingTraps;
    public bool isWaitingNextWave;

    [Header("Local onde vai o grid da fase")]
    //Talvez fazer uma array para poder por grids d tamanhos diferentes
    public Grid grid;
    public Trap[] traps;
    [Range(0, 10)]
    public int selectedTrap;

    public GameObject previewTrapObject;
    [Range(0, 3)]
    [HideInInspector]
    public float trapsRotation;
    public float funds;
    [HideInInspector] public PlayerFunds playerFunds;


    [HideInInspector] public RaycastHit hitInfo;
    [HideInInspector] public RaycastHit[] lateralHitInfo;
    public Ray[] rayDirections;
    [HideInInspector] public Vector3[] directions;
    public LayerMask playerMask = 1;
    public LayerMask sphereCastMask = 2;

    public GameUIManager gameUIManager;
    public Slider selectedTrapImage;
    public Text costText;

    private bool selectedTrapHorizontal;
    private bool selectedTrapVertical;
    private int usingTrap = -1;
    private float custo;
    [SerializeField]
    private GameObject previewTrap;
    // Start is called before the first frame update
    void Start()
    {
        MainData.LoadData();
        MainData.placingTraps = true;
        isPlacingTraps = MainData.placingTraps;
        gameUIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<GameUIManager>();
        playerFunds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFunds>();
        costText.text = (traps[selectedTrap].cost - traps[selectedTrap].modCost * MainData.upgrades[traps[selectedTrap].trapID]).ToString();
        lateralHitInfo = new RaycastHit[4];
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        custo = traps[selectedTrap].cost - (traps[selectedTrap].modCost * MainData.upgrades[traps[selectedTrap].trapID]);
        isPlacingTraps = MainData.placingTraps;
        isWaitingNextWave = MainData.waitingNextWave;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MainData.placingTraps = !MainData.placingTraps;
            gameUIManager.ModeUIChange();
            if(previewTrap != null)
            {
                if (previewTrap != null)
                {
                    gameUIManager.UpdatePlaceTrapText("");
                    Destroy(previewTrap);
                    return;
                }
            }
        }

        if (isPlacingTraps == true)
        {
            ChangeTraps();
            
            if (Input.GetKeyDown(KeyCode.E) && previewTrap != null)
            {
                //RaycastHit hitInfo;
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float custo = (traps[selectedTrap].cost - traps[selectedTrap].modCost);
                if (custo <= funds)
                {
                    if (MainData.canUseTrap[traps[selectedTrap].trapID] == 1) {
                        funds -= custo;
                        playerFunds.AtualizarHud();
                        PlaceTrapNear(hitInfo.point);
                    }
                }
                else
                {
                    Debug.Log("You don't have enough funds");
                }
            }
        }
        
    }

    public void PlacePreview()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Color[] colors = new Color[] { Color.yellow, Color.red, Color.blue, Color.green };

        if (Physics.Raycast(ray, out hitInfo, 20, playerMask) && (hitInfo.collider.CompareTag("Ground") || hitInfo.collider.CompareTag("Wall")))
        {
            //Debug.Log(hitInfo.normal);
            var finalposition = grid.GetNearestPointOnGrid(hitInfo.point);
            //Debug.Log(finalposition);
            //trecho que cuida das bordas
            if (hitInfo.normal.y != 0)
            {
                directions = new Vector3[] { finalposition + Vector3.forward + hitInfo.normal, finalposition + hitInfo.normal + Vector3.right, finalposition + hitInfo.normal + Vector3.left, finalposition + hitInfo.normal + Vector3.back };
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
                if (Physics.Raycast(directions[i], -hitInfo.normal, out lateralHitInfo[i], 1f, playerMask)/* && (lateralHitInfo[i].collider.CompareTag(usingtag))*/)
                {
                    //Debug.DrawRay(directions[i], -hitInfo.normal, colors[i]);
                    SameTag++;
                    //Debug.Log("Raycast " + i + " Mesma tag e achou algo" + lateralHitInfo[i].collider.tag);
                }
                else
                {
                    /*Debug.DrawRay(directions[i], -hitInfo.normal, colors[i]);
                    Debug.Log("Raycast " + i + " achou nada");*/
                    return;
                }
            }

            if (SameTag >= 4)
            {
                //checagem de outras traps na area
                Collider[] hitColliders = Physics.OverlapSphere(finalposition, grid.size / 4, sphereCastMask, QueryTriggerInteraction.Ignore);

                for (int t = 0; t < hitColliders.Length; t++)
                {
                    if (hitColliders[t].tag == "Trap" || hitColliders[t].tag == "Obstacle")
                    {
                        if (previewTrap != null)
                        {
                            gameUIManager.UpdatePlaceTrapText("");
                            Destroy(previewTrap);
                            return;
                        }
                        if(hitColliders[t].tag == "Trap" && isWaitingNextWave ==true)
                        {
                            gameUIManager.UpdatePlaceTrapText("Press 'Q' To sell trap");
                            if (Input.GetKeyDown(KeyCode.Q))
                            {
                                Debug.Log(hitColliders[t].gameObject);
                                Trap retorno = hitColliders[t].gameObject.GetComponent<Trap>();
                                funds += Mathf.Round(custo / 2); ;
                                playerFunds.AtualizarHud();
                                Destroy(hitColliders[t].gameObject);
                                return;
                            }
                        }

                            return;
                    }

                    if (usingtag == "Ground" && hitColliders[t].tag == "Wall" || usingtag == "Wall" && hitColliders[t].tag == "Ground")
                    {
                        if (previewTrap != null)
                        {
                            gameUIManager.UpdatePlaceTrapText("");
                            Destroy(previewTrap);
                            return;

                        }
                        return;
                    }

                    if (usingtag == "Wall" && hitColliders[t].tag == "Wall")
                    {
                        if (hitInfo.collider.gameObject != hitColliders[t].gameObject) {
                            if (previewTrap != null)
                            {
                                gameUIManager.UpdatePlaceTrapText("");
                                Destroy(previewTrap);
                                return;

                            }
                            return ;
                        }
                    }

                }
                //Debug.Log(hitInfo.normal);
                //Debug.Log(hitInfo.collider.tag);

                //instancia o preview ou muda a sua posição
                if (previewTrap == null)
                {
                    if (selectedTrapHorizontal == true && hitInfo.collider.CompareTag("Ground"))
                    {
                        if (hitInfo.normal.y != 0)
                        {
                            previewTrap = Instantiate(traps[selectedTrap].preview, finalposition, Quaternion.identity);

                            previewTrap.transform.localScale = traps[selectedTrap].horDimentions;
                        }
                    }
                    else if (selectedTrapVertical == true && hitInfo.collider.CompareTag("Wall"))
                    {
                        if (hitInfo.normal.y == 0)
                        {
                            previewTrap = Instantiate(traps[selectedTrap].preview, finalposition /*+ hitInfo.normal * (0.1f)*/, Quaternion.identity);
                            previewTrap.transform.localScale = traps[selectedTrap].vertDimentions;
                            previewTrap.transform.up = hitInfo.normal;
                        }
                    }
                    else
                    {
                        return;
                    }
                    gameUIManager.UpdatePlaceTrapText("Press 'E' to place trap");
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
                        previewTrap.transform.position = finalposition + hitInfo.normal * (0.1f);


                        if (hitInfo.normal.x != 0)
                        {
                            if (hitInfo.normal.x > 0)
                            {
                                previewTrap.transform.up = hitInfo.normal;
                                previewTrap.transform.rotation = Quaternion.Euler(previewTrap.transform.rotation.eulerAngles + (new Vector3(-90, 0, 0) * trapsRotation) + new Vector3(90, 0, 0));
                            }
                            else if (hitInfo.normal.x < 0)
                            {
                                previewTrap.transform.up = hitInfo.normal;
                                previewTrap.transform.rotation = Quaternion.Euler(previewTrap.transform.rotation.eulerAngles + (new Vector3(90, 0, 0) * trapsRotation) + new Vector3(90, 0, 0));
                            }

                        }
                        else if (hitInfo.normal.z != 0)
                        {
                            if (hitInfo.normal.z > 0)
                            {
                                previewTrap.transform.up = hitInfo.normal;
                                previewTrap.transform.rotation = Quaternion.Euler(previewTrap.transform.localRotation.eulerAngles + new Vector3(90 * trapsRotation, 90, 90)/*+ new Vector3(-90 * trapsRotation, 90, 90)*/);
                            }
                            else if (hitInfo.normal.z < 0)
                            {
                                previewTrap.transform.up = hitInfo.normal;
                                previewTrap.transform.rotation = Quaternion.Euler(previewTrap.transform.localRotation.eulerAngles + new Vector3(90 * trapsRotation, 90, 90) + new Vector3(180, 180, 0));
                            }
                        }

                    }
                    gameUIManager.UpdatePlaceTrapText("Press 'E' to place trap");
                }
            }
        }
        else if (previewTrap != null)
        {
            usingTrap = selectedTrap;
            //Debug.Log("Raycast deu hein" + hitInfo.collider.tag);
            gameUIManager.UpdatePlaceTrapText("");
            Destroy(previewTrap);
            return;
        }
        //mouse
        /*if (Input.GetKeyDown(KeyCode.E) && previewTrap != null)
        {
            //RaycastHit hitInfo;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (traps[selectedTrap].cost <= funds)
            {
                funds -= traps[selectedTrap].cost;
                playerFunds.AtualizarHud();
                PlaceTrapNear(hitInfo.point);
            } else
            {
                Debug.Log("You don't have enough funds");
            }
        }*/
    }



    public void PlaceTrapNear(Vector3 clickPoint)
    {
        var finalposition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject armadilha = Instantiate(traps[selectedTrap].gameObject, previewTrap.transform.position, previewTrap.transform.rotation);

        //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = clickPoint;
    }

    public void ChangeTraps()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 && selectedTrap < traps.Length - 1)
        {
            selectedTrap += 1;
            selectedTrapImage.value = selectedTrap;

            costText.text = (traps[selectedTrap].cost - (traps[selectedTrap].modCost * MainData.upgrades[traps[selectedTrap].trapID])).ToString();
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0 && selectedTrap > 0)
        {
            selectedTrap -= 1;
            selectedTrapImage.value = selectedTrap;
            costText.text = (traps[selectedTrap].cost - (traps[selectedTrap].modCost * MainData.upgrades[traps[selectedTrap].trapID])).ToString();
        }

        if (usingTrap == selectedTrap)
        {
            PlacePreview();
        }
        else
        {
            usingTrap = selectedTrap;
            selectedTrapHorizontal = traps[selectedTrap].horizontal;
            selectedTrapVertical = traps[selectedTrap].vertical;
            Destroy(previewTrap);
            return;
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (trapsRotation < 3)
            {
                trapsRotation += 1;
            }
            else
            {
                trapsRotation = 0;
            }
        }
    }

    public void AddFunds(float fundos)
    {
        funds += fundos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(Camera.main.transform.position, hitInfo.point);
    }
}
