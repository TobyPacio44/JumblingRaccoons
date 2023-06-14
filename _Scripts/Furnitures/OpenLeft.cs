using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLeft : MonoBehaviour
{
    Quaternion Open = Quaternion.Euler(0, 90, 0);
    Quaternion Closed;
    public Collider disable;
    public bool isOpened;

    private void Start()
    {
        Closed = this.transform.rotation;
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
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Closed * Open, 0.05f);
        disable.enabled = false;
    }

    private void CloseDown()
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Closed, 0.05f);
        disable.enabled = true;
    }
}
