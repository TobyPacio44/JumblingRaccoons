using System;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class Movement : MonoBehaviour
{
    [Header("Player")]
    public GameObject raccoon; //physical body of Raccoon
    [SerializeField] private Player player; //player main script

    public Transform startingRot; //camera1
    public Transform orientation;
    private Rigidbody rb;

    [Header("Rotation")]
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    [Header("Movement")]
    public float moveSpeed = 4500;
    public float Speed;
    public float baseSpeed;
    public float sprintSpeed;
    public float climbSpeed = 5.0f;
    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle;
    public float maxHeightJump;
    public LayerMask whatIsGround;
    public LayerMask whatToClimb;

    [Header("Sprinting")]
    public Slider sprintUI;

    [Header("Jumping")]
    public float jumpForce = 550f;
    public float gravity;
    private bool readyToJump = true;
    private float jumpCooldown = 0.15f;

    [HideInInspector] public bool canClimb = false;
    [HideInInspector] public bool isClimbing = false;
    private float timeSinceJump = 0.0f;
    private float CurrentY;
    private float StartingY;

    [HideInInspector] public bool grounded;
    [HideInInspector] public bool canJump = true;
    private bool isFalling;

    [Header("Input")]
    float x, y;
    [HideInInspector] public bool jumping, sprinting;
    [HideInInspector] public bool pause;

        private Vector3 normalVector = Vector3.up;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        if(isClimbing) {             
            Climb();
            return;
        }

        PlayerMovement();
        //Disable sprint when jumping
        if (!grounded)
        {
            Speed = baseSpeed;
        }
    }

    private void Update()
    {
        MyInput();
        Look();

        timeSinceJump += Time.deltaTime;
        CurrentY = this.transform.position.y;

        if (grounded)
        {
            StartingY = this.transform.position.y;
        }
        if (CurrentY - StartingY >= maxHeightJump)
        {
            isFalling = true;
        }
        if (CurrentY == StartingY)
        {
            isFalling = false;
        }
        if (isFalling)
        {
            Vector3 vel = rb.velocity;
            rb.velocity = new Vector3(vel.x, gravity, vel.z);
        }
        if (!Input.GetButton("Jump") && CurrentY - StartingY >= 7)
        {
            isFalling = true;
        }
    }

    private void MyInput()
    {
        if (pause)
        {
            return;
        }
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");

        if (Input.GetKey(KeyCode.LeftShift) && sprintUI.value > 0.1f && grounded)
        {
            Speed = sprintSpeed;
            sprinting = true;
        }
        else if (grounded)
        {
            Speed = baseSpeed;
            sprinting = false;
        }

    }

    private void PlayerMovement()
    {
        //Extra gravity
        if (!grounded && !canJump)
        {
            Vector3 vel = rb.velocity; 
            rb.velocity = new Vector3(vel.x*0.9f, gravity, vel.z*0.9f); 
        }

        //Find actual velocity relative to where player is looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding and sloppy movement
        CounterMovement(x, y, mag);

        //Some multipliers
        float multiplier = 8f;
        float multiplierV = 1f;

        // Movement in air
        if (!grounded)
        {
            multiplier = 4f;
            multiplierV = 1f;
        }

        //Apply forces to move player
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);

        //If holding jump && ready to jump, then jump
        if (Input.GetButton("Jump") && !isFalling)
        {
            if (timeSinceJump > jumpCooldown)
            {
                canJump = true;
                Jump();
            }
        }

        //Set max speed
        float maxSpeed = this.Speed;

        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        
        //Sprint UI
        if (sprinting && grounded)
        {
            sprintUI.value -= 0.015f;
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        { sprintUI.value += 0.045f; }

        

        //Is falling

        if (timeSinceJump < jumpCooldown)
        {
            canJump = false;
        }
        if (isFalling)
        {
            timeSinceJump = 0;
        }
        if (Input.GetButtonDown("Jump"))
        {
            isFalling = false;
        }
        
        /* if (Input.GetButtonUp("Jump") && !grounded)
        {
            isFalling = true;
        } */
    }

    private void Jump()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;
            //Add jump forces
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            /* If jumping while falling, reset y velocity.
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z); */

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void Climb()
    {
        Transform up = player.Crosshair.up;
        Transform down = player.Crosshair.down;
        Transform whereLand = player.Crosshair.whereLand;

        float verticalInput = Input.GetAxis("Vertical");
        this.rb.useGravity = false;
        player.Camera.RotatingAroundPlayer = true;

        if (raccoon.transform.position.y < down.transform.position.y)
        {
            ClimbDestination();
            raccoon.transform.position = down.transform.position;
            return;
        }

        if (raccoon.transform.position.y > up.transform.position.y)
        {
            ClimbDestination();
            raccoon.transform.position = whereLand.transform.position;
            return;
        }

        // Calculate the vertical movement to apply to the Rigidbody
        Vector3 climbMovement = Vector3.up * verticalInput * climbSpeed * Time.deltaTime;

        // Apply the movement to the Rigidbody
        rb.velocity = climbMovement;

        rb.AddForce(orientation.transform.up * y * climbSpeed * Time.deltaTime);        

        if (Input.GetKey(KeyCode.LeftShift))
        {

            player.animations.state = Animations.Animation.idle;
            ClimbDestination();
            return;
        }
    }

    public void ClimbDestination()
    {
        player.Crosshair.ResetAction();
        player.animations.state = Animations.Animation.idle;
        GetComponent<Collider>().enabled = true;
        isClimbing = false;
        player.Rig.transform.localRotation = Quaternion.Euler(0, 0, 0);
        this.rb.useGravity = true;
        player.Camera.RotatingAroundPlayer = false;
        player.Crosshair.ResetCamera();
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = startingRot.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);

    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping) return;

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }
        // Limit diagonal running.
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > Speed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * Speed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        } 
    }
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;
    private void OnCollisionStay(Collision other)
    {
        int layer = other.gameObject.layer;

        //Make sure we are only checking for walkable layers
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }

    public void Stop()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, gravity, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuyMenu"))
        {
            other.GetComponent<BuyBuilding>().purchasePrompt = player.ShopUI;
            other.GetComponent<BuyBuilding>().SetMenuText(player.ShopUI);
            player.ShopUI.SetActive(true);
        }
    }

}