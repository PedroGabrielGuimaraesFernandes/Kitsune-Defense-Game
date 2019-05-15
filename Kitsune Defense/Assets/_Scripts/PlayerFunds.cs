using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFunds : MonoBehaviour
{
    public float playerFunds;
    public TrapPlacer bank;
    public Text text;


    private void Start()
    {
        bank = GameObject.FindGameObjectWithTag("TrapPlacer").GetComponent<TrapPlacer>();
        playerFunds = bank.funds;
        text.text = playerFunds.ToString();
    }


    public void AtualizarHud()
    {
        playerFunds = bank.funds;
        text.text = playerFunds.ToString();
    }

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
            bank.AddFunds(5);
            // atualiza o hud 
            AtualizarHud();

            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag("Green Orb"))
        {
            bank.AddFunds(15);
            // atualiza o hud 
            AtualizarHud();

            Destroy(hit.gameObject);
        }
        if (hit.gameObject.CompareTag("Orb"))
        {
            bank.AddFunds(50);
            // atualiza o hud 
            AtualizarHud();

            Destroy(hit.gameObject);
        }
    }
    
    

}
