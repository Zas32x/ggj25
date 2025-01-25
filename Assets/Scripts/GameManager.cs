using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager inst = null;

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

    public void FadeActionHandler(int level)
    {
        if (level < 0)
        {
            Application.Quit();
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }

}
