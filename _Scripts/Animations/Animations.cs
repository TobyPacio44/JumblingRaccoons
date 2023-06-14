using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Animations : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    public  Animator anim;

    public Movement movement;

    public bool isGrounded;
    public bool isjumping;
    public bool emoting;

    public enum Animation
    {
        idle,
        climbing,
        jumping,
        running,
        walking,
    }
    public Animation state;


    void Start()
    {      
        anim = GetComponent<Animator>();
        state = Animation.idle;
    }
    private void Update()
    {
        switch (state)
        {
            case Animation.idle:
                anim.Play("Base Layer.idle");
                break;
            case Animation.climbing:
                anim.Play("Base Layer.climb");
                if (Input.GetKeyDown(KeyCode.W))
                {
                    anim.Play("Base Layer.climb");
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    anim.Play("Base Layer.climb2");
                }

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
                {
                    anim.speed = 1;
                }
                else
                {
                    anim.speed = 0;
                }

                break;
        }      

    }

    void FixedUpdate()
    {
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.Play("Paws.grab");
            }

            if (Input.GetAxis("Jump") > 0 && !isjumping && movement.canJump)
            {
                state = Animation.jumping;
                isjumping = true;
                anim.Play("Base Layer.jump");
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
            emoting = true;
            anim.Play("Base Layer.dance");
            }

            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
            emoting = false;
            }

            if (movement.grounded && !isjumping && !emoting)
            {
                isjumping = false;
                if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
                {
                     anim.Play("Base Layer.idle"); 
                }
                if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") == 0 && movement.sprinting == false)
                {
                     state = Animation.walking;
                     anim.Play("Base Layer.walk"); 
                }
                if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") == 0 && movement.sprinting)
                {
                    
                    state = Animation.running;
                    anim.Play("Base Layer.run"); 
                }
                if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") != 0)
                {
                    
                    state = Animation.walking;
                    anim.Play("Base Layer.walk"); 
                }
                if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") != 0)
                {
                    
                    state = Animation.walking;
                    anim.Play("Base Layer.walk"); 
                }
                if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") == 0)
                {
                    
                    state = Animation.walking;
                    anim.Play("Base Layer.walk2"); 
                }
                if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") != 0)
                {
                    
                    state = Animation.walking;
                    anim.Play("Base Layer.walk2"); 
                }

            }
          

            if (movement.grounded == true)
            {
                isjumping = false;
            }
            else isjumping = true;

    }

}
