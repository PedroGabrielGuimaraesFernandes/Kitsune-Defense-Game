using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trap: MonoBehaviour {
    public Animator anim;
    [Header("Numero de Identificação da trap no main data")]
    public int trapID;
    /*[Header("Indica se a trap esta ou não liberada pro player, 0 ñ lieberada e 1 liberada")]
    [Range(0,1)]
    public int isUnlocked;*/
    [Header("Objeto de preview e imagem de representação da trap")]
    public GameObject preview;
    public Sprite trapImage;
    [Header("Tipo de possição que a trap pode ser colocada")]
    public bool horizontal;
    public bool vertical;

    [Header("Dimenções da trap na Horizontal")]
    public Vector3 horDimentions;

    [Header("Dimenções da trap na Vertical")]
    public Vector3 vertDimentions;

    //public GameObject objeto;
    //Dano da trap
    public float damage = 50;
    // Tempo de recarga da trap
    public float reloadTime = 1f;
    //Custo pra coloca-la
    public int cost = 100;
    //Custo de Unlock
    public int unlockCost = 100;
    [Header("Modificadores do upgrade")]
    public float modDamage;
    public float modReload;
    public int modCost;
    //se esta recaregando ou ñ
    protected bool reloading;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(MainData.upgrades[trapID] > 0)
        {
            damage = damage + modDamage * MainData.upgrades[trapID];
            reloadTime = reloadTime - modReload * MainData.upgrades[trapID];
            cost = cost - modCost * MainData.upgrades[trapID];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CauseDamage(IABase inimigo)
    {
        //Inimigo enemyScript = other.transform.GetComponent<Inimigo>();
        inimigo.TakeDamage(damage);
        //fazer o resto da trap
    }

    /*private void OnTriggerEnter(Collider other)
    {
        IABase enemyScript = other.transform.GetComponent<IABase>();
        CauseDamage(enemyScript);

    }*/

}
