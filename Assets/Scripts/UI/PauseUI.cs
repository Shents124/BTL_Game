using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitPause()
    {
        GameManager.Instance.ExitPauseGame();
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
