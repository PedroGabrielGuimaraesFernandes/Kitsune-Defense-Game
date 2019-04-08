using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    public GameObject nextImage; 


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextImage() {
        if (!nextImage)
        {
            MainData.NextScene = "Menu";

            SceneManager.LoadScene("load");
        }
        else
        {
            nextImage.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
