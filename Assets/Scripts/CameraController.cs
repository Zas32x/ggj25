using UnityEngine;

public class CameraController : MonoBehaviour
{
    float rotationX = 0f;
    float rotationY = 0f;

    public Vector2 sensitivity = Vector2.one * 360f;

    [SerializeField] GameObject pivot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity.x;
        rotationX += Input.GetAxis("Mouse Y") * Time.deltaTime * -1 * sensitivity.y;
        pivot.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}
