using UnityEngine;

public class SeeThroughPoint : MonoBehaviour
{
    [SerializeField] Material material;
    
    void Update() {
        UpdateMaterial();
    }

    private void UpdateMaterial() {
        material.SetVector("_lookAt", transform.position);
    }
}
