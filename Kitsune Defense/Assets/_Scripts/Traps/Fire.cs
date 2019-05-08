using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Particula Colidiu");
            IABase enemyScript = other.GetComponent<IABase>();
            enemyScript.CauseBurnDamage(5,5);

            return;
        }
    }
}
