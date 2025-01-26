using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text progress;
    [SerializeField] private TMP_Text progressLabel;
    [SerializeField] private float doneTextSeconds=7;

    [SerializeField] private CompletionText[] completions;
    
    private IEnumerator currentEnumeration = null;
    private float lastPercentage = 0;
    
    void Update()
    {
        if (Input.GetButton("ContinueGame") || Input.GetButton("QuitGame"))
        { 
            progressLabel.gameObject.SetActive(false);
        }
    }

    public void UpdateProgress(float progressAmount)
    { 
        progress.text = $"{(progressAmount*100):0}%";
        for (int i = 0; i < completions.Length; i++)
        { 
            CompletionText completion = completions[i];
            if (completion.percentage > lastPercentage && completion.percentage <= progressAmount)
            {
                ShowProgress(completion, completion.keepVisible || progressAmount >= 1 ? 0 : doneTextSeconds);
                break;
            }
        }

        lastPercentage = progressAmount;
    }

    private void ShowProgress(CompletionText completion, float hideTime)
    {
        progressLabel.gameObject.SetActive(true);
        progressLabel.text = completion.text;
        if (currentEnumeration != null)
        { 
            StopCoroutine(currentEnumeration);
        }
        if (hideTime > 0)
        {
            StartCoroutine(currentEnumeration = HideTextTimer(hideTime));
        }
    }

    private IEnumerator HideTextTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        progressLabel.gameObject.SetActive(false);
        currentEnumeration = null;
    }

    [Serializable]
    public struct CompletionText { 
        public string text;
        public float percentage;
        public bool keepVisible;
    }
}
