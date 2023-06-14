using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLoop2 : MonoBehaviour
{
    [SerializeField] private Player player; //player main script
    public Transform PlayerCam;  //this cam   
    public Transform Orientation;

    // Mouse floats are dafault camera movement
    [HideInInspector] public float MouseX;
    [HideInInspector] public float MouseY;

    // MouseLooking are 'F' mode camera movement
    [HideInInspector] public float MouseLookingY;
    [HideInInspector] public float MouseLookingX;

    public float sens;
    public Slider xSens;

    [HideInInspector] public bool ScrolledUp;
    [HideInInspector] public bool tabMenu;

    [HideInInspector] public bool RotatingAroundPlayer;
     public Vector3 offset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //camera distance on free look
        offset = new Vector3(-2f, 10f, -45f);
    }

    void Update()
    {     
        if (tabMenu) 
        { 
            return; 
        }      
        if (RotatingAroundPlayer)
        {
            RotateAroundPlayer();
            return;
        }

        Rotate();
        offset = transform.position - player.movement.orientation.transform.position;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ScrolledUp = true;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ScrolledUp = false;
            PlayerCam.transform.localPosition = new Vector3(0.40f, 16.81f, -22.26f);
        }

        if (ScrolledUp)
        {
            LookAround();
        }
    }

    private void Rotate()
    {
        MouseX += Input.GetAxis("Mouse X") * xSens.value;
        MouseY += Input.GetAxis("Mouse Y") * xSens.value;
        MouseY = Mathf.Clamp(MouseY, -18, 90);

        MouseLookingY += Input.GetAxis("Mouse Y") * xSens.value;
        MouseLookingY = Mathf.Clamp(MouseLookingY, -75, 40);

        MouseLookingX += Input.GetAxis("Mouse X") * xSens.value;
        MouseLookingX = Mathf.Clamp(MouseLookingX, 0, 0);

        player.raccoon.transform.rotation = Quaternion.Euler(0, MouseX, 0);
        PlayerCam.rotation = Quaternion.Euler(-MouseY, MouseX, 0);
    }
    private void RotateAroundPlayer()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f, Vector3.up) * offset;
        transform.position = player.raccoon.transform.position + offset;

        if (player.Crosshair.FakeParent == null)
        {
            transform.LookAt(Orientation);
        }
        else { transform.LookAt(player.Crosshair.FakeParent); }
        return;
    }
    private void LookAround()
    {
         PlayerCam.transform.localPosition = new Vector3(0.07f, 17.76f, 0.99f);
         PlayerCam.transform.localRotation = Quaternion.Euler(-MouseLookingY, MouseLookingX, 0) ;        
    }
}
