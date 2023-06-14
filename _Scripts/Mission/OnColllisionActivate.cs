using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColllisionActivate : MonoBehaviour
{
    public GameObject activate;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") { 
            activate.SetActive(true);
            this.GetComponent<Collider>().enabled = false;
        }
        
    }
}
