using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCopController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotSpeed = 5;
    public Transform side;
    public Transform rangeEffectMuzzle;
    public GameObject rangeAttackEffect;
    public Rigidbody rangeAttackBall;
    public Transform meleeEffectMuzzle;
    public GameObject meleeAttackEffect;
    //public Rigidbody meleeAttackBall;

    private Transform trans;

    private Animator anim;
    private int moveIndex;
    private int sprintIndex;
    //private int attackIndex;
    private int meleeAttackIndex;
    private int rangeAttackIndex;
    private int moveMeleeAttackIndex;
    private int moveRangeAttackIndex;
    private int damageIndex;
    private int deadIndex;
    private int speedIndex;
    private bool canMove;
    private bool canAttack;
    private bool takeHit;
    private bool canDie;
    private bool canRespawn;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        moveIndex = Animator.StringToHash("move");
        sprintIndex = Animator.StringToHash("sprint");
        meleeAttackIndex = Animator.StringToHash("meleeAttack");
        rangeAttackIndex = Animator.StringToHash("rangeAttack");
        moveMeleeAttackIndex = Animator.StringToHash("moveMeleeAttack");
        moveRangeAttackIndex = Animator.StringToHash("moveRangeAttack");
        damageIndex = Animator.StringToHash("damage");
        deadIndex = Animator.StringToHash("dead");
        speedIndex = Animator.StringToHash("speed");
        canMove = true;
        canAttack = true;
        takeHit = true;
        canDie = true;
    }

    // Update is called once per frame
    void Update()
    {
        float m = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Horizontal");
        bool s = Input.GetKey(KeyCode.LeftShift) ? true : false;

        if (Input.GetKeyDown(KeyCode.F) && takeHit)
        {
            anim.SetTrigger(damageIndex);
            canAttack = false;
            canMove = false;
            takeHit = false;
        }

        if (Input.GetKeyDown(KeyCode.M) && canDie)
        {
            anim.SetTrigger(deadIndex);
            canAttack = false;
            canMove = false;
            takeHit = false;
            canDie = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m != 0.0f)
            {
                anim.SetLayerWeight(1, 1.0f);
                anim.SetTrigger(moveMeleeAttackIndex);
                canAttack = false;
            }
            else
            {
                anim.SetTrigger(meleeAttackIndex);
                canAttack = false;
            }
            takeHit = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (m != 0.0f)
            {
                anim.SetLayerWeight(1, 1.0f);
                anim.SetTrigger(moveRangeAttackIndex);
                canAttack = false;
            }
            else
            {
                anim.SetTrigger(rangeAttackIndex);
                canAttack = false;
            }

            /*anim.SetTrigger(rangeAttackIndex);
            canAttack = false;
            canMove = false;*/
            takeHit = false;
        }
        /*if(Mathf.Abs(r) >= 0.2f)
        {
            //trans.Rotate(trans.rotation.x, side.rotation.y,trans.rotation.z);

            transform.rotation = Quaternion.Euler(trans.rotation.x, side.rotation.y, trans.rotation.z);
            Debug.Log(side.localRotation.y);
        }*/


        trans.Rotate(0, r * rotSpeed * Time.deltaTime, 0);

        anim.SetFloat(speedIndex, Mathf.Sign(m));
        anim.SetBool(moveIndex, Mathf.Abs(m) >= 0.2F);
        anim.SetBool(sprintIndex, s);
    }

    public void CreateAttackEffect()
    {
        var pos = rangeEffectMuzzle.position;
        var rot = gameObject.transform.rotation;

        Instantiate(rangeAttackEffect, pos, rot);
        var b = Instantiate(rangeAttackBall, pos, rot) /*as Rigidbody*/;
        b.AddForce(rangeEffectMuzzle.forward * 500);
    }

    public void FirePunch()
    {
        var pos = meleeEffectMuzzle.position;
        var rot = meleeEffectMuzzle.rotation;

        Instantiate(meleeAttackEffect, pos, rot);
        //var b = Instantiate(meleeAttackBall, pos, rot)/* as Rigidbody*;
    }


    public void AllowMovement()
    {
        anim.SetLayerWeight(1, 0.0f);
        canAttack = true;
        takeHit = true;
    }

    public void Recovery()
    {
        takeHit = true;
        canAttack = true;
        canMove = true;
    }

    public void Death()
    {
        Destroy(gameObject, 1);
        return;
    }

    public void AllowRespawn()
    {
        canRespawn = true;
    }

    public void Respawn()
    {

        canMove = true;
        canAttack = true;
        takeHit = true;
        canDie = true;
        canRespawn = false;
    }

}

