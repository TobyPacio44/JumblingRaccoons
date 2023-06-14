using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //Locker is the door that is locker
    public GameObject Locker;
    public void Unlocking()
    {
        Locker.GetComponent<Locker>().Unlocking();
    }

}
