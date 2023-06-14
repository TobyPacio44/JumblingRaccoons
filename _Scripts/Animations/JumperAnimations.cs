using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAnimations : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] LayerMask playerMask;
    private Animator anim;
    public bool isLeft;
    public bool isGrounded;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isLeft == true && Input.GetAxisRaw("Horizontal") == 0 && isGrounded == true)
        {
            anim.Play("Base Layer.idle_left");
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && isLeft == false && isGrounded == true)
        {
            anim.Play("Base Layer.idle");
        }

        if (Input.GetAxis("Jump") > 0 && isLeft == false && !isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") < 0) { anim.Play("Base Layer.jump_left"); }
            else
            anim.Play("Base Layer.jump");           
        }

        if (Input.GetAxis("Jump") > 0 && isLeft == true && !isGrounded)
        {
            if (Input.GetAxisRaw("Horizontal") > 0) { anim.Play("Base Layer.jump"); }
            else
                anim.Play("Base Layer.jump_left");
        }

        if (isGrounded == false && Input.GetAxisRaw("Horizontal") < 0)
        {
            anim.Play("Base Layer.jump_left");
        }

        if (isGrounded == false && Input.GetAxisRaw("Horizontal") > 0)
        {
            anim.Play("Base Layer.jump");
        }

        if (Input.GetAxisRaw("Horizontal") > 0 && isGrounded == true)
        {
            anim.Play("Base Layer.walk");
            isLeft = false;
        }

        if (Input.GetAxisRaw("Horizontal") < 0 && isGrounded == true)
        {
            anim.Play("Base Layer.walk_left");
            isLeft = true;
        }


        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }

    }

}
