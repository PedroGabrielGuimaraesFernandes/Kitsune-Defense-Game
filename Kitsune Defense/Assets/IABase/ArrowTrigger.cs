using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    IAArcher ArcherReference;
    Rigidbody ArrowRB;

    void Start()
    {
        ArcherReference = transform.GetComponentInParent<IAArcher>();
        DestroyAfterSeconds(3);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Objective"))
        {
            ArcherReference.DealDamage();
        }
        if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterSeconds(float LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
