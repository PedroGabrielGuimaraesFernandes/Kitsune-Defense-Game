using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : Trap
{
    public Transform reference;
    public ParticleSystem[] shootPosition;
    public float maxNumShoot=12;

    private int attackIndex;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //attackIndex = Animator.StringToHash("attack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        for (int i = 0; i < maxNumShoot; i++)
        {
            int r = Random.Range(0, 12);
            shootPosition[r].Emit(1);
            //Debug.Log("Particule System" + r + "para i num" + i);
        }
        ReloadTrap(reloadTime);
    }

    public void ReloadTrap(float tempo)
    {
        StartCoroutine(ReloadTimer(tempo));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gameObject.transform.position,gameObject.transform.position + reference.transform.up * 6);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.CompareTag("Enemy") && reloading == false) {
            reloading = true;

            Debug.Log("Kunai Trap Activated");
            StartCoroutine(ShootShuriken());
            //Shoot();
            //anim.SetTrigger(attackIndex);
        }
        /*IABase enemyScript = other.transform.GetComponent<IABase>();
        CauseDamage(enemyScript);*/
    }

    public IEnumerator ShootShuriken()
    {
        int r = 0;
        //vai se repetir a te dar o tempo do reload
        while (r < shootPosition.Length)
        {
            shootPosition[r].Emit(1);
            r++;
            //Debug.Log("Particule System" + r + "para i num" + r);
            yield return new WaitForSeconds(0.1f);

        }
        ReloadTrap(reloadTime);
    }

    public IEnumerator ReloadTimer(float time)
    {
        float r = 0;
        //vai se repetir a te dar o tempo do reload
        while (r < reloadTime)
        {
            r++;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Relodou");
        reloading = false;
    }
}
