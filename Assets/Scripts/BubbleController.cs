using System;
using UnityEngine;

public class BubbleController : MonoBehaviour
{

    Rigidbody rb;
    float speed = 10;
    float maxSpeed = 50;
    float fallSpeed = 3;
    Vector3 velocity;


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
            rb.AddForce(Camera.main.transform.right * Input.GetAxis("Horizontal") * speed, ForceMode.Acceleration);
        }
        if (Input.GetButton("Vertical"))
        {
            rb.AddForce(Camera.main.transform.forward * Input.GetAxis("Vertical") * speed, ForceMode.Acceleration);
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
        velocity = rb.linearVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA velocity: " + rb.linearVelocity + " vs. " + velocity);
        rb.linearVelocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
    }
}
