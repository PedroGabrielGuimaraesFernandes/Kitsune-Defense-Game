using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockButtons : MonoBehaviour {
    public Button[] buttons;
    private void Awake()
    {
        MainData.LoadData();
    }

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            if (MainData.levelStatus[i] > 0)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
}
