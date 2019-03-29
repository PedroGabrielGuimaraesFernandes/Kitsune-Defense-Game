using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunds : MonoBehaviour
{
    public float playerFunds;
    public TrapPlacer bank;

    private void Start()
    {
        bank = GameObject.FindGameObjectWithTag("TrapPlacer").GetComponent<TrapPlacer>();
        playerFunds = bank.funds;
    }


    /*public void AtualizarHud()
    {

    }*/

    /* void OnCollisionEnter(Collision other)
    {
        Debug.Log("colidiu");
        if (other.gameObject.CompareTag("Orb"))
        {
            Debug.Log("colidiu mas ñ destruiu");
            bank.AddFunds(5);
            // atualiza o hud 
            //playerFunds = bank.funds;
            Destroy(other.gameObject);
        }
    }*/



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Orb"))
        {
            Debug.Log("colidiu mas ñ destruiu");
            bank.AddFunds(5);
            // atualiza o hud 
            //playerFunds = bank.funds;
            Destroy(hit.gameObject);
        }
    }
    
    

}
