using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public GameObject locker_object;
    public bool isLocked;

    public void Unlocking()
    {
        isLocked = false;
        Destroy(locker_object);
    }
}
