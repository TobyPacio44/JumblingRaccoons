using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDown : MonoBehaviour
{
    public bool isOpened;
    Quaternion Open = Quaternion.Euler(-90, 0, 0);
    Quaternion Closed;
    public Collider disable;

    private void Start()
    {
        Closed = this.transform.localRotation;
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
        this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Open * Closed, 0.05f);
        disable.enabled = false;
    }

    private void CloseDown()
    {
        this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Closed, 0.05f);
        disable.enabled = true;
    }
}
