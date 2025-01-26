using UnityEngine;
using FMOD;
using FMODUnity;

public class ambienceTrigger : MonoBehaviour
{

    [SerializeField] StudioEventEmitter ambientSound;
    [SerializeField] int parameter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ambientSound.SetParameter("Space", parameter);
        }
    }
}
