using System;
using UnityEngine;

public class BubbleController : MonoBehaviour
{

    Rigidbody rb;
    float speed = 10;
    float maxSpeed = 100;
    float fallSpeed = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Horizontal"))
        {
            rb.AddForce(new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0), ForceMode.Acceleration);
        }
        if (Input.GetButton("Vertical"))
        {
            rb.AddForce(new Vector3(0, 0, Input.GetAxis("Vertical") * speed), ForceMode.Acceleration);
        }
        if (Input.GetButton("Jump"))
        {
            rb.AddForce(new Vector3(0, speed, 0), ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(new Vector3(0, -fallSpeed, 0), ForceMode.Acceleration);
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }
}
