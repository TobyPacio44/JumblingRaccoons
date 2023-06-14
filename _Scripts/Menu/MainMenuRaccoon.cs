using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRaccoon : MonoBehaviour
{
    private Vector3 positions;

    void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(0.5f, 0, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            
            transform.position = transform.position - new Vector3(360, 0, 0);
        }
       
    }


}
