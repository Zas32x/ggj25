using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public float fadeTime = 1.0f;
    private Image image;
    
    [SerializeField] float fadeInTime = 0f;

    void Start()
    {
        image = GetComponent<Image>();
        if (fadeInTime >= 0) {
            StartFadeIn();
        }
    }

    public void StartFade(int level)
    {
        StartCoroutine(FadeOut(level));
    }

    IEnumerator FadeOut(int level)
    {
        float t = 0.0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            image.color = new Color(0, 0, 0, t / fadeTime);
            yield return null;
        }

        image.color = Color.black;
        GameManager.Instance.FadeActionHandler(level);
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn(fadeInTime));
    }
    
    IEnumerator FadeIn(float initialDelay)
    {
        float t = -initialDelay;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            image.color = new Color(0, 0, 0,Mathf.Clamp( 1 - t / fadeTime,0,1));
            yield return null;
        }

        image.color = Color.clear;
    }
}
