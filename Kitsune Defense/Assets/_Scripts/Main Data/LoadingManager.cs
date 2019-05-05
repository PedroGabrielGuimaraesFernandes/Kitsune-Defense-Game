using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {


    AsyncOperation aop;
    public GameObject barra;
    float SmoothProgress = 0;

    // Use this for initialization
    void Start () {
        aop = SceneManager.LoadSceneAsync(MainData.NextScene);
        print(MainData.NextScene);
        aop.allowSceneActivation = false;

    }

    // Update is called once per frame
    void Update () {

        SmoothProgress = Mathf.MoveTowards(SmoothProgress, aop.progress, Time.deltaTime * 2);

        barra.transform.localScale = new Vector3(SmoothProgress * 11.5f/* 10.25 */, barra.transform.localScale.y, 1);

        if (SmoothProgress > 0.89f)
        {
            //print(aop.progress);
            aop.allowSceneActivation = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
}
