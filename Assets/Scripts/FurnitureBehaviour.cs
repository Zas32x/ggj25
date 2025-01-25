using System.Transactions;
using UnityEngine;

public class FurnitureBehaviour : MonoBehaviour
{
    [SerializeField] Collider coll;
    GameObject container;
    Rigidbody rb;
    float floatForce = 45;

    bool underCenter = false;
    float underCenterDrag = 3;
    float underCenterAngularDrag = 1;
    float overCenterDrag = 0;
    float overCenterAngularDrag = 0.05f;


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
            //float heightDifference = transform.position.y - container.GetComponent<Collider>().bounds.center.y;

            //if (heightDifference < 0)
            //{
            //    rb.AddForceAtPosition(Vector3.up * floatForce * Mathf.Abs(heightDifference), transform.position, ForceMode.Force);
            //    if (!underCenter)
            //    {
            //        underCenter = true;
            //        SwitchFloatMode(underCenter);
            //    }
            //}
            //else if (underCenter)
            //{
            //    underCenter = false;
            //    SwitchFloatMode(underCenter);
            //}

            Debug.Log("Floating");
            rb.AddForce((container.transform.position - transform.position) * floatForce, ForceMode.Acceleration);
            rb.AddForce(container.GetComponent<Rigidbody>().linearVelocity, ForceMode.Force);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.bounds.Contains(coll.bounds.max) && other.bounds.Contains(coll.bounds.min))
            {
                container = other.gameObject;
                //rb.useGravity = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            container = null;
            //rb.useGravity = true;
        }
    }

    private void SwitchFloatMode(bool isUnderCenter)
    {
        if (isUnderCenter)
        {
            rb.linearDamping = underCenterDrag;
            rb.angularDamping = underCenterAngularDrag;
        }
        else
        {
            rb.linearDamping = overCenterDrag;
            rb.angularDamping = overCenterAngularDrag;
        }
    }
}
