using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text progress;
    [SerializeField] private TMP_Text done70;
    [SerializeField] private TMP_Text done100;
    [SerializeField] private float doneTextSeconds=7;

    private bool shown70;
    private bool shown100;
    
    void Start()
    {
    }

    public void UpdateProgress(float progressAmount, float victoryPercentage)
    { 
        progress.text = $"{(progressAmount*100):0}%";
        if (progressAmount >= victoryPercentage && !shown70) {
            shown70 = true;
            ShowProgress(done70, doneTextSeconds);
        }
        if (progressAmount >= 1 && !shown100) {
            done70.gameObject.SetActive(false);
            shown100 = true;
            ShowProgress(done100);
        }
    }

    private void ShowProgress(TMP_Text text, float hideTime=0)
    {
        text.gameObject.SetActive(true);
        if (hideTime > 0)
        {
            StartCoroutine(HideTextTimer(text, hideTime));
        }
    }

    private IEnumerator HideTextTimer(TMP_Text text, float duration)
    {
        yield return new WaitForSeconds(duration);
        text.gameObject.SetActive(false);
    }
}
