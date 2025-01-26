using TMPro;
using UnityEngine;

public class IntroText : MonoBehaviour {
    [SerializeField] private TMP_Text text;
    [SerializeField] private float duration=7;
    [SerializeField] private float fadeTime=1;

    private float time;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float alpha = Mathf.Clamp( 1 - Mathf.Clamp(time -duration, 0, 1) / fadeTime, 0, 1);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        if (time >= duration + fadeTime) { 
            gameObject.SetActive(false);
        }
    }
}
