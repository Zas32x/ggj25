using System.Collections;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField]
    public float endGameTimeout = 10f;
    [SerializeField]
    public float allowMovementAfterTime = 10f;
    [SerializeField]
    private HUD hud;
    [SerializeField]
    private FadeController fadeController;
    [SerializeField]
    private float victoryPercent=0.75f;

    private BubbleController bubble;
     
    private bool victoryAchieved = false;

    private int furnitureCount = 0;
    private int furnitureMovedOut = 0;
    
        bool ending;
        
    private static PlayManager _instance;
    public static PlayManager Instance { 
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PlayManager>();
            }
            return _instance;
        }
    }

    public void Start()
    {
        bubble = FindAnyObjectByType<BubbleController>();
        StartCoroutine(AllowMovement());
    }

    private IEnumerator AllowMovement() {
        yield return new WaitForSeconds(allowMovementAfterTime);
        bubble.AllowMovement();
    }

    public void Update()
    {
        if (victoryAchieved && !ending)
        {
            if (Input.GetButton("ContinueGame"))
            {
                ending = true;
            } else if (Input.GetButton("QuitGame"))
            {
                ending = true;
                StartCoroutine(EndLevel(0));
            }
        }
    }

    public void RegisterFurniture()
    {
        ++furnitureCount;
    }

    public void FurnitureMovedOut()
    {
        ++furnitureMovedOut;
        float percentDone = furnitureMovedOut / (float)furnitureCount;
        hud?.UpdateProgress(percentDone);
        if (furnitureMovedOut >= furnitureCount)
        {
            StartCoroutine(EndLevel(endGameTimeout));
        }
        if (percentDone >= victoryPercent && victoryAchieved == false)
        {
            victoryAchieved = true;
            bubble.EnoughMovedOut();
        }
    }

    private IEnumerator EndLevel(float endGameTimeout) {
        ending = true;
        
        yield return new WaitForSeconds(endGameTimeout);

        fadeController.StartFade(0);
    }
}
