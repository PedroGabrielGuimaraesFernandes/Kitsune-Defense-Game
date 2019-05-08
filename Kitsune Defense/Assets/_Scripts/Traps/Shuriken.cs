using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
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
            IABase enemyScript = other.transform.GetComponent<IABase>();
            enemyScript.TakeDamage(2);
            
            return;
        }
        //Debug.Log("particulacolidiu");
    }

    /*public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy")) {
            Debug.Log("Particula Colidiu");
        IABase enemyScript = other.transform.GetComponent<IABase>();
        enemyScript.TakeDamage(2);
        Destroy(gameObject);
            }
    }*/
}
