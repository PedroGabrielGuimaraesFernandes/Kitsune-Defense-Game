using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTrigger : MonoBehaviour {

    public void LoadScene(string Scene)
    {
        MyLoad.Loading(Scene);
    }

}

