using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {

    public int LancerQuantity;
    public int ArcherQuantity;
    public int SamuraiQuantity;
    [HideInInspector]
    public int totalQuantity;
    public bool useSpawnPoint1;
    public bool useSpawnPoint2;

    /*public int Total(int lancer, int archer, int samurai)
    {
        totalQuantity = LancerQuantity + ArcherQuantity + SamuraiQuantity;

        return totalQuantity; 
    }*/

}
