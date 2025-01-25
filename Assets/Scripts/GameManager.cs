using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager inst = null;

    private int furnitureCount = 0;
    private int furnitureMovedOut = 0;
    
    public static GameManager Instance
    {
        get
        {
            return inst;
        }
    }

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterFurniture()
    {
        ++furnitureCount;
    }

    public void FadeActionHandler(int level)
    {
        if (level < 0)
        {
            Application.Quit();
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }

    public void FurnitureMovedOut()
    {
        --furnitureMovedOut;
        if (furnitureMovedOut <= 0)
        {
            // win at life
        }
    }
}
