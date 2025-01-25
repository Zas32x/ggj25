using System.Transactions;
using UnityEngine;

public class FurnitureBehaviour : MonoBehaviour
{
    [SerializeField] Collider coll;
    GameObject container;
    Rigidbody rb;
    float floatForce = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (container != null)
        {
            Debug.Log("Floating");
            rb.AddForce((container.transform.position - transform.position) * floatForce, ForceMode.Acceleration);
            rb.AddForce(container.GetComponent<Rigidbody>().linearVelocity, ForceMode.Acceleration);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.bounds.Contains(coll.bounds.max) && other.bounds.Contains(coll.bounds.min))
            {
                container = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            container = null;
        }
    }
}
