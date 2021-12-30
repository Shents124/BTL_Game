using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            else
            {
                _instance = FindObjectOfType<GameManager>();
                return _instance;
            }
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ExitPauseGame()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

}
