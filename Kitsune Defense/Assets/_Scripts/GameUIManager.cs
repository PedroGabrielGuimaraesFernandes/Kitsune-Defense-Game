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
    public Text startWave;
    [Header("Other objects")]
    public PlayerController player;
    public MainObjectiveManager objective;
    public PauseManager pauseControl;
    public RiceGain riceCheck;
    public GameObject trapPlacer;
    public GameObject cameraMoviment;
    public GameObject trapUI;
    public GameObject combatUI;

    // outras varriaveis proprias
    public bool levelEnded;
    // Start is called before the first frame update
    void Start()
    {
        riceCheck = gameObject.GetComponent<RiceGain>();
        MainData.wonLevel = false;
        victoryCanvas.SetActive(false);
        defeatCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Victory();
        }
    }

    public void UpdatePlaceTrapText(string texto)
    {
        placeTrapText.text = texto;
    }

    public void Victory()
    {
        MainData.wonLevel = true;
        riceCheck.CheckRice();
        victoryCanvas.SetActive(true);
        cameraMoviment.SetActive(false);
        trapPlacer.SetActive(false);
        riceCheck.ShowRiceGained();
        player.EndOfLevel();
    }

    public void Defeat(/*string name/* nome do objeto que tá chamando o void*/)
    {
       
        defeatCanvas.SetActive(true);
        cameraMoviment.SetActive(false);
        trapPlacer.SetActive(false);
        player.EndOfLevel();
    }

    public void ModeUIChange()
    {
        if(MainData.placingTraps != true)
        {
            trapUI.SetActive(false);
            combatUI.SetActive(true);
        }
        else
        {
            trapUI.SetActive(true);
            combatUI.SetActive(false);
        }
    }

    public void WavesBegan()
    {
        startWave.text = "";
    }
}
