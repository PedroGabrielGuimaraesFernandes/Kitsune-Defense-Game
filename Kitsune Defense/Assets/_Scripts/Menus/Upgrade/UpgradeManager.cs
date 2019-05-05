using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Coisas de comprar a trap")]
    public Image[] trapsToBuy;
    public Color unlockColor;
    public int unlockPrice = 2;
    public GameObject[] cadeado;
    [Header("Coisas do Upgrade")]
    // objeto com as marcas
    public GameObject upgradeObject;
    //Preço do upgrade
    public int upgradePrice = 1;
    // as posições q as marcas podem ficar
    public Transform[] upgradePositions;
    // as marcas em si
    public Image[] upgradeMark;
    // as core de ativado ou ñ
    public Color upgradeColor1;
    public Color upgradeColor2;

    public GameObject upgradeInformation;
    public Trap[] trapArray;
    public Text upgradeCostText;
    public Text riceOwned;
    public Text damageStatsText;
    public Text reloadStatsText;
    public Text costStatsText;
    public Text trapDescriptionText;



    // Start is called before the first frame update
    void Start()
    {
        MainData.LoadData();
        for (int i = 0; i < MainData.canUseTrap.Length; i++)
        {
            if (MainData.canUseTrap[i] == 0)
            {
                Debug.Log("Color Change");
                trapsToBuy[i].color = unlockColor;
                //trapsToBuy[i].SetActive(false);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCostAndGain(int trapid)
    {
        upgradeInformation.SetActive(true);
        if (MainData.canUseTrap[trapid] == 0)
        {
            upgradeCostText.text = "Price: " + unlockPrice.ToString() + " Rice";
            damageStatsText.text = "";
            reloadStatsText.text = "";
            costStatsText.text = "";
            trapDescriptionText.text = MainData.trapDescription[trapid];
            Debug.Log("is buing trap");
            //riceOwned.text = "You got " + MainData.arrozTotal.ToString();
        }
        else
        {
            upgradeCostText.text = "Price: " + upgradePrice.ToString() + " Rice";
            if (trapArray[trapid].modDamage != 0)
            {
                float valor1 = trapArray[trapid].damage + trapArray[trapid].modDamage * MainData.upgrades[trapid];
                float valor2 = trapArray[trapid].damage + trapArray[trapid].modDamage * (MainData.upgrades[trapid] + 1);
                damageStatsText.text = "Damage " + valor1.ToString() + " to " + valor2.ToString();
            }
            else
            {
                float valor1 = trapArray[trapid].damage + trapArray[trapid].modDamage * MainData.upgrades[trapid];
                damageStatsText.text = "Damage " + valor1.ToString();
            }
            if (trapArray[trapid].modReload != 0)
            {
                float valor3 = trapArray[trapid].reloadTime - trapArray[trapid].modReload * MainData.upgrades[trapid];
                float valor4 = trapArray[trapid].reloadTime - trapArray[trapid].modReload * (MainData.upgrades[trapid] + 1);
                reloadStatsText.text = "Reload " + valor3.ToString() + " to " + valor4.ToString() +" Seconds";
            }
            else
            {
                float valor3 = trapArray[trapid].reloadTime - trapArray[trapid].modReload * MainData.upgrades[trapid];
                reloadStatsText.text = "Reload " + valor3.ToString() + " Seconds";
            }
            if (trapArray[trapid].modCost != 0)
            {
                float valor5 = trapArray[trapid].cost - trapArray[trapid].modCost * MainData.upgrades[trapid];
                float valor6 = trapArray[trapid].cost - trapArray[trapid].modCost * (MainData.upgrades[trapid] + 1);
                costStatsText.text = "Cost " + valor5.ToString() + " to " + valor6.ToString() + " Souls";
            }
            else
            {
                float valor5 = trapArray[trapid].cost - trapArray[trapid].modCost * MainData.upgrades[trapid];
                costStatsText.text = "Cost " + valor5.ToString() +" Souls";
            }
            trapDescriptionText.text = MainData.trapDescription[trapid];
            Debug.Log("is buing upgrade");
        }

        riceOwned.text = "You got: " + MainData.arrozTotal.ToString() + " Rice";
    }

    public void DeactivateCostAndGain(int trapid)
    {
        if (MainData.canUseTrap[trapid] == 1)
        {
            upgradeInformation.SetActive(false);
            upgradeObject.SetActive(true);
        }
    }

    public void CheckIfUnlocked(int trapid)
    {
        if (MainData.canUseTrap[trapid] ==1)
        {
            CheckUlnlockedUpgrades(trapid);
            cadeado[trapid].SetActive(false);
        }
        else
        {
            cadeado[trapid].SetActive(true);
        }

    }

    public void CheckUlnlockedUpgrades(int trapid)
    {
        upgradeObject.SetActive(true);
        for (int i = 0; i < upgradeMark.Length; i++)
        {
            upgradeMark[i].color = upgradeColor1;
        }
        Debug.Log("CheckUlnlockedUpgrades");
        upgradeObject.transform.position = upgradePositions[trapid].position;
        for (int i = 0; i < MainData.upgrades[trapid]; i++)
        {
            upgradeMark[i].color = upgradeColor2;
        }
    }

    public void BuyTrap(int trapid)
    {
        if (MainData.arrozTotal >= unlockPrice)
        {
            MainData.arrozTotal -= unlockPrice;
            MainData.canUseTrap[trapid] = 1;
            MainData.SaveData();
            riceOwned.text = "You got " + MainData.arrozTotal.ToString() + "Rice";
            CheckIfUnlocked(trapid);
        }
    }

    public void BuyUpgrade(int trapid)
    {
        if (MainData.arrozTotal >= upgradePrice)
        {
            MainData.arrozTotal -= upgradePrice;
            MainData.upgrades[trapid] += 1;
            MainData.SaveData();
            riceOwned.text = MainData.arrozTotal.ToString();
            CheckUlnlockedUpgrades(trapid);
        }
    }

    /*public void UpdateUI()
    {
    }*/
}
