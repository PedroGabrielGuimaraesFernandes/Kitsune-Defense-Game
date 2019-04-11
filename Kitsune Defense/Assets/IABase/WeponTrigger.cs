using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponTrigger : MonoBehaviour
{
    IAMelee Reference;

    void Start()
    {
        Reference = transform.GetComponentInParent<IAMelee>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Objective"))
        {
            Reference.DealDamage();
        }
    }
}
