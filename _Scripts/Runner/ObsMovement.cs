using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMovement : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
