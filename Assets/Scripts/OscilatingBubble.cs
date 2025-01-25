using UnityEngine;

public class OscilatingBubble : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float speed = 5.0f;
    private Vector3 scaleAmplitude;
    private Vector3 startScale;
    private float offset;
    private float usedSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScale = transform.localScale;
        scaleAmplitude = startScale * amplitude;
        offset = Random.Range(-Mathf.PI, Mathf.PI);
        usedSpeed = speed + offset / Mathf.PI;
    }

    // Update is called once per frame
    void Update()
    {
        float time = (Time.time + offset) * usedSpeed;
        float sin = Mathf.Sin(time);
        float cos = Mathf.Cos(time);
        transform.localScale = startScale + new Vector3(scaleAmplitude.x * sin, scaleAmplitude.y * cos, scaleAmplitude.z * sin * cos);
    }
}
