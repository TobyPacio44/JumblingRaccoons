using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPull : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 0.05f;
    public bool isOpened;
    public Transform furniture;
    public Collider disable;

    private void Start()
    {
        isOpened = false;
    }
    void Update()
    {
        if (isOpened)
        {
            OpenUp();
        }

        if (!isOpened)
        {
            CloseDown();
        }
    }

    private void OpenUp()
    {
        furniture.position = Vector3.Lerp(furniture.position, pointB.transform.position, speed);
        disable.enabled = false;
    }

    private void CloseDown()
    {
        furniture.position = Vector3.Lerp(furniture.position, pointA.transform.position, speed);
        disable.enabled = true;
    }
}
