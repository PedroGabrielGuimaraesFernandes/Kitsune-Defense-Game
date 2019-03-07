using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour {

    public float speed = 4;
    public float rotationSpeed = 18;
    float rot = 0;
    public float gravity = 8;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        Movement();
        GetInput();
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetAxis("Horizontal") == 0)
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
            }
            else if (Input.GetAxis("Horizontal") != 0 && anim.GetBool("attacking") == false)
            {
                anim.SetBool("running", true);
                anim.SetInteger("condition", 1);
            }
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
        }
        rot += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(anim.GetBool("running") == true)
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                }
                if (anim.GetBool("running") == false)
                {
                    Attacking();
                }
                
            }
        }
    }

    void Attacking()
    {
        
        StartCoroutine(AttackRoutine());
      
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
    }
}
