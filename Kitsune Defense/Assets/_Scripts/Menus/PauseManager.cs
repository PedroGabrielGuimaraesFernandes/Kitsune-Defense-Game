using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    //public GameObject pauseCanvas;
    public Canvas pauseCanvas;
    public Canvas playerCanvas;
    public GameObject player;
    public TrapPlacer trapPlacer;
    //public GameObject cameraObj;

    public GameObject painelInicial;
    public GameObject painelSom;
    public GameObject botoesMenu;
    public GameObject botoesQuit;
    public GameUIManager gameIsOver;
    public MixerLevels soundVolumeCtrl;

    public GameObject dialogue;
    public bool inDialogue;
    GameObject[] dots;

    public float focus_distance;
    public float focal_length;


    // Use this for initialization
    private void Awake()
    {
        pauseCanvas.enabled = false;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        trapPlacer = GameObject.FindGameObjectWithTag("TrapPlacer").GetComponent<TrapPlacer>();
        soundVolumeCtrl = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MixerLevels>();
        pauseCanvas.enabled = false;
        inDialogue = false;

    }

    // Update is called once per frame
    void Update()
    {
        //inDialogue /*= inDialogue*/ = dialogue.GetComponent<DialogueManager>().inDialogue;

        if (Input.GetKeyDown(KeyCode.Escape) && inDialogue == false && gameIsOver.levelEnded == false ||
            Input.GetKeyDown(KeyCode.P) && inDialogue == false && gameIsOver.levelEnded == false)
        {
            if (pauseCanvas.enabled == false)
            {
                Pause();

            }
            else
            {
                Continue();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.R))
        {
            GameOver();
        }*/

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        pauseCanvas.enabled = true;

        playerCanvas.enabled = false;

        //FindObjectOfType<TimeManager>().ChangeTimeScale(0);
        Time.timeScale = 0f;
        trapPlacer.enabled = false;

        //player.GetComponent<MouseLook>().enabled = false;
        //cameraObj.GetComponent<ThirdPersonCameraFollow>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }


    public void Continue()
    {
        //pauseCanvas.SetActive(false);
        pauseCanvas.enabled = false;
        playerCanvas.enabled = true;
        painelInicial.SetActive(true);
        painelSom.SetActive(false);
        botoesMenu.SetActive(false);
        botoesQuit.SetActive(false);
        //FindObjectOfType<TimeManager>().ChangeTimeScale(1);
        Time.timeScale = 1;
        trapPlacer.enabled = true;
        //player.GetComponent<MouseLook>().enabled = true;
        //cameraObj.GetComponent<ThirdPersonCameraFollow>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    public void MusicVolume(float volume)
    {
        soundVolumeCtrl.SetMusicLev(volume);
    }

    public void SFXVolume(float volume)
    {
        soundVolumeCtrl.SetSFXLev(volume);
    }
}
