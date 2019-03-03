using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifeTime = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);        
    }

}
