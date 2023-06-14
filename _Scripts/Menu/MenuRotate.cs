using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotate : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
             float h = horizontalSpeed * Input.GetAxis("Mouse X");       
             this.transform.Rotate(0, -h, 0);
        }       
    }
}
