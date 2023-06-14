using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor: MonoBehaviour
{
    public Quaternion Open = Quaternion.Euler(0, -90, 0);
    private Quaternion Closed;
    public Collider disable;
    public bool isOpened;

    private Locker locker;


    private void Start()
    {
        Closed = this.transform.rotation;
        isOpened = false;
        if(GetComponent<Locker>() != null)
        {
            locker = GetComponent<Locker>();
        }
    }

    public void Interact()
    {
        if (locker != null)
        {
            if (!locker.isLocked)
            {
                if (!isOpened) { isOpened = true; }
                else { isOpened = false; }
            }
        }
        else
        {
            if (!isOpened) { isOpened = true; }
            else { isOpened = false; }
        }
    }
    private void Update()
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
