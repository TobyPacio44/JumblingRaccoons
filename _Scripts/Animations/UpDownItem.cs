using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownItem : MonoBehaviour    
{
    public AnimationCurve myCurve;
    public float RotationSpeed;
    void Update()
    {
        
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
        transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime));
    }
}
