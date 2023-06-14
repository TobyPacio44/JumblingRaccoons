using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RunnerMovement : MonoBehaviour
{
    [SerializeField] private Transform GroundCheck;
    [SerializeField] LayerMask playerMask;
    Rigidbody rb;

    public Points PointAmount;

    private float points;
    private bool jumpKeyWasPressed;
    public float JumpPower = 25f;
    private float id = 3;  
    public float movespeed = 1;
    public Vector3 userDirection = Vector3.right;
    private Animator anim;

    public GameObject CanvasMain;
    public GameObject TabMenu;
    public MouseLoop2 Mouse;

    [SerializeField] TextMeshProUGUI score_points;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        PointAmount = GameObject.FindGameObjectWithTag("PointsAmount").GetComponent<Points>();
        score_points.text = PointAmount.points.ToString();        
    }
    public void FixedUpdate()
    {
        if (Physics.OverlapSphere(GroundCheck.position, 0.1f, playerMask).Length == 0)
        {
           return;                    
        }
               
        if (jumpKeyWasPressed)
        {
            rb.AddForce(0, JumpPower, 0, ForceMode.Impulse);
            jumpKeyWasPressed = false;
        }
        
        }
        public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
            anim.Play("Base Layer.jump");
        }
              
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (id == 5)
            {
                return;
            }
            transform.position = transform.position + new Vector3(10, 0, 0);
            id++;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (id == 1)
            {
                return;
            }
            transform.position = transform.position - new Vector3(10, 0, 0);
            id--;
        }


        transform.Translate(userDirection * movespeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
            
            }
        if (CanvasMain.activeSelf==true)
            {
                Time.timeScale = 1;
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            SceneManager.LoadScene(2);
        }
        if (other.gameObject.layer == 9)
        {
            SceneManager.LoadScene(1);
        }
        if (other.gameObject.layer == 10)
        {
            SceneManager.LoadScene(1);
        }
        if (other.gameObject.layer == 3)
        {
            Destroy(other.gameObject);

            PointAmount.points += 100;          
            score_points.text = PointAmount.points.ToString();
        }
    }
}
