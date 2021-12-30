using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController _instance;
    public static SceneController Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            else
            {
                _instance = FindObjectOfType<SceneController>();
                return _instance;
            }
        }
    }
    public CanvasGroup loadingCanvas;
    private int isLoadTutorial;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("isLoadTutorial") == false)
            isLoadTutorial = 0;
        else
            isLoadTutorial = PlayerPrefs.GetInt("isLoadTutorial");
    }
    public void LoadSceneWithName()
    {
        StartCoroutine(Transition());
        if (isLoadTutorial == 0)
        {
            isLoadTutorial = 1;
            PlayerPrefs.SetInt("isLoadTutorial", isLoadTutorial);
        }
    }

    IEnumerator Transition()
    {
        string sceneName;
        if (isLoadTutorial == 0)
            sceneName = "Tutorial";
        else
            sceneName = "MainLevel";

        loadingCanvas.gameObject.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 0.5f));
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (operation.isDone == false)
            yield return null;
        yield return StartCoroutine(FadeLoadingScreen(0, 0.5f));
        loadingCanvas.gameObject.SetActive(false);
    }

    IEnumerator FadeLoadingScreen(float finalAlpha, float duration)
    {
        float fadeSpeed = Mathf.Abs(loadingCanvas.alpha - finalAlpha) / duration;
        while (Mathf.Approximately(loadingCanvas.alpha, finalAlpha) == false)
        {
            loadingCanvas.alpha = Mathf.MoveTowards(loadingCanvas.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        loadingCanvas.alpha = finalAlpha;
    }

}
