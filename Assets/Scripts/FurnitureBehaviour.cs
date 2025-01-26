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
    
    bool sentOut = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        PlayManager.Instance.RegisterFurniture();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (container != null)
        {
            float heightDifference = transform.position.y - container.GetComponent<Collider>().bounds.center.y;

            if (heightDifference < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatForce * Mathf.Abs(heightDifference), transform.position, ForceMode.Force);
                if (!underCenter)
                {
                    underCenter = true;
                    SwitchFloatMode(underCenter);
                }
            }
            else if (underCenter)
            {
                underCenter = false;
                SwitchFloatMode(underCenter);
            }

            Debug.Log("Floating");
            //rb.AddForce((container.transform.position - transform.position) * floatForce, ForceMode.Acceleration);
            Vector3 horizontalVelocity = new Vector3(container.transform.position.x - transform.position.x, 0, container.transform.position.z - transform.position.z); //new Vector3(container.GetComponent<Rigidbody>().linearVelocity.x, 0, container.GetComponent<Rigidbody>().linearVelocity.z);
            rb.AddForce(horizontalVelocity * 50.2f, ForceMode.Force);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="Player")
        {
            if (other.bounds.Contains(coll.bounds.max) && other.bounds.Contains(coll.bounds.min))
            {
                container = other.gameObject;
                //rb.useGravity = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal" && !sentOut)
        {
            sentOut = true;
            PlayManager.Instance.FurnitureMovedOut();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.isTrigger)
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
