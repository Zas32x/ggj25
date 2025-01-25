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
        ++furnitureMovedOut;
        Debug.Log($"Furniture Moved Out: {furnitureMovedOut} / {furnitureCount}");
        if (furnitureMovedOut >= furnitureCount)
        {
            Debug.Log("All Furniture Moved Out");
            // win at life
        }
    }
}
