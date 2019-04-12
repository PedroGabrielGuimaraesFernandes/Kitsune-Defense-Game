using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetAll()
    {
        MainData.ResetAll();
    }

    public void ResetLevels()
    {
        MainData.ResetLevels();
    }


    public void ResetTraps()
    {
        MainData.ResetTraps();
    }

    public void ResetArroz()
    {
        MainData.ResetArroz();
    }
}
