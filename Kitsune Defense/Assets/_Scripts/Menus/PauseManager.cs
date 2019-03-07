using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public GameObject pauseCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Continue()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

}
