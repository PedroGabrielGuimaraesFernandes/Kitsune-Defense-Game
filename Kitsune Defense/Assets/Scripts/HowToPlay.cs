using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour {

    public GameObject[] instruction;
    //public int instructionNub;
    public int actualInstruction = 0;
    public GameObject leftArrow;
    public GameObject rightArrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(actualInstruction > instruction.Length)
        {
            actualInstruction = instruction.Length - 1;
        }
		if(actualInstruction <= 0)
        {
            leftArrow.SetActive(false);
        }else if (actualInstruction > 0)
        {
            leftArrow.SetActive(true);
        }
        if (actualInstruction < instruction.Length - 1) {
            rightArrow.SetActive(true);
        }
        else if (actualInstruction >= instruction.Length - 1)
        {
            rightArrow.SetActive(false);
        }

    }

    public void NextRight()
    {
        actualInstruction++;
        instruction[actualInstruction].SetActive(true);
        instruction[actualInstruction - 1].SetActive(false);
    }

    public void NextLeft()
    {
        actualInstruction--;
        instruction[actualInstruction].SetActive(true);
        instruction[actualInstruction + 1].SetActive(false);
    }
}
