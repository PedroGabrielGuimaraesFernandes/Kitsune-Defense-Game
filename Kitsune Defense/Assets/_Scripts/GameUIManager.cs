using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header ("Panels")]
    public GameObject victoryCanvas;
    public GameObject defeatCanvas;
    [Header("Texts")]
    public Text placeTrapText;
    [Header("Other objects")]
    public PlayerController player;
    public MainObjectiveManager objective;
    public PauseManager pauseControl;
    public GameObject cameraMoviment;

    // outras varriaveis proprias
    public bool levelEnded;
    // Start is called before the first frame update
    void Start()
    {
        victoryCanvas.SetActive(false);
        defeatCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlaceTrapText(string texto)
    {
        placeTrapText.text = texto;
    }

    public void Victory()
    {
        victoryCanvas.SetActive(true);
        cameraMoviment.SetActive(false);
        player.EndOfLevel();
    }

    public void Defeat(/*string name/* nome do objeto que tá chamando o void*/)
    {
       
        defeatCanvas.SetActive(true);
        cameraMoviment.SetActive(false);
        player.EndOfLevel();
    }
}
