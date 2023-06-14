using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Transform PlayerCam;
    public Transform player;
    public float MouseX;
    public float MouseY;
    public float sens;
    public MouseLoop2 MouseLoop2;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseX += Input.GetAxis("Mouse X") * MouseLoop2.xSens.value;
        MouseY = Mathf.Clamp(MouseY, -90, 90)* MouseLoop2.xSens.value;
        player.rotation = Quaternion.Euler(0, MouseX, 0);
        PlayerCam.rotation = Quaternion.Euler(-MouseY, MouseX, 0);
    }

}
