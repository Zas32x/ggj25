using System.Collections;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField]
    public float victoryPercentage = 0.7f;
    [SerializeField]
    public float endGameTimeout = 10f;
    [SerializeField]
    private HUD hud;
    [SerializeField]
    private FadeController fadeController;
     
    private bool victoryAchieved = false;

    private int furnitureCount = 0;
    private int furnitureMovedOut = 0;
    
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
        fadeController.StartFadeIn();
    }

    public void RegisterFurniture()
    {
        ++furnitureCount;
    }

    public void FurnitureMovedOut()
    {
        ++furnitureMovedOut;
        hud?.UpdateProgress(furnitureMovedOut/(float)furnitureCount, victoryPercentage);
        if (furnitureMovedOut >= furnitureCount)
        {
            StartCoroutine(EndLevel());
        }
    }

    private IEnumerator EndLevel() {
        yield return new WaitForSeconds(endGameTimeout);

        fadeController.StartFade(0);
    }
}
