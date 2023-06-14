using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMovementJumper : MonoBehaviour
{
    public float upSpeed = 3f;
    public float rightSpeed = 7f;

    private void Update()
    {
        transform.position += Vector3.up * upSpeed * Time.deltaTime;
        transform.position += Vector3.right * rightSpeed * Time.deltaTime;
    }


}
