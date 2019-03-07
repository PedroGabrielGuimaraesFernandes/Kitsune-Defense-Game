using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour {

    public float speed;
    public float fireRate;
    public GameObject muzzelPrefab;
    public GameObject hitPrefab;


	// Use this for initialization
	void Start () {
        if (muzzelPrefab != null)
        {
            var muzzelVFX = Instantiate(muzzelPrefab,transform.position,Quaternion.identity);
            muzzelVFX.transform.forward = gameObject.transform.forward;
            var psMuzzel = muzzelVFX.GetComponent<ParticleSystem>();
            if (psMuzzel != null)
            {
                Destroy(muzzelPrefab, psMuzzel.main.duration);
            }
            else
            {
                var psChild = muzzelVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzelVFX, psChild.main.duration);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime); 
        }
        else
        {
            Debug.Log("No Speed");
        }
	}

    private void OnCollisionEnter(Collision other)
    {
        speed = 0;

        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if (psHit != null)
            {
                Destroy(hitPrefab, psHit.main.duration);
            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }

        
        Destroy(gameObject);
        Debug.Log("colidiu");

    }
}
