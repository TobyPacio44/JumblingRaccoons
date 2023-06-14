using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skateboard : MonoBehaviour
{
    private Rigidbody rb;
    private float rotationSpeed = 0.8f;
    public float moveSpeed;
    public float maxSpeed;
    float y;
    public GameObject child;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (!child)
        {
            return;
        }
        movement();
    }

    void movement()
    {
        float multiplier = 8f;
        float maxSpeed = this.maxSpeed;
        Vector2 mag = FindVelRelativeToLook();
        float yMag = mag.y;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        y = Input.GetAxisRaw("Vertical");
        rb.AddForce(transform.forward * y * moveSpeed * Time.deltaTime * multiplier);

        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, rotationSpeed * Input.GetAxis("Horizontal"), 0f));

    }
    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = child.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }
}
