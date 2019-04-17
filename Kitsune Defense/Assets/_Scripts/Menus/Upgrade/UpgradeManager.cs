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
    public Text upgradeCostText;
    public Text riceOwned;
   


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
            upgradeCostText.text = "Price: " + unlockPrice.ToString();
        }
        else
        {
            upgradeCostText.text = "Price: " + upgradePrice.ToString();
        }

        riceOwned.text = MainData.arrozTotal.ToString();
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
            riceOwned.text = MainData.arrozTotal.ToString();
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
