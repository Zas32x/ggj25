using UnityEngine;

public class BubbleController : MonoBehaviour
{

    Rigidbody rb;
    float speed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("horizontal"))
        {
            rb.AddForce(new Vector3(Input.GetAxis("horizontal") * speed, 0, 0));
        }
        if (Input.GetButton("vertical"))
        {
            rb.AddForce(new Vector3(0, Input.GetAxis("vertical") * speed, 0));
        }
    }
}
