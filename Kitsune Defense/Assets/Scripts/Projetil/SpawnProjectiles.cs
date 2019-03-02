using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour {
    [Header("Possição inicial do disparo")]
    public GameObject firePoint;
    [Header("Lista de efeitos possiveis")]
    public List<GameObject> vfx = new List<GameObject>();
    [Header("Script de referencia para a direção do disparo")]
    public RotateToMouse rotateToMouse;
    
    //efeito a ser spawnado
    private GameObject effectToSpawn;
    //tempo entre os tiros (rate of fire)
    private float timeToFire = 0;

	// Use this for initialization
	void Start () {
        effectToSpawn = vfx[0];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
            SpawnVFX();
        }
	}

    public void SpawnVFX()
    {
        GameObject vfx;

        if (firePoint != null)
        {
            vfx = Instantiate(effectToSpawn,firePoint.transform.position,Quaternion.identity);
            if(rotateToMouse != null)
            {
                vfx.transform.rotation = rotateToMouse.GetRotation();
            }
        }
        else
        {
            Debug.Log("Sem Fire Point");
        }
    }
}
