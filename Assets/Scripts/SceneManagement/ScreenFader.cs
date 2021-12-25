using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    public enum FadeType
    {
        Black, GameOver,
    }

    private static ScreenFader _instance;
    public static ScreenFader Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            else
            {
                _instance = FindObjectOfType<ScreenFader>();
                return _instance;
            }
        }
    }

    private bool isFading;
    public static bool IsFading => Instance.isFading;

    public CanvasGroup faderCanvasGroup;
    public CanvasGroup gameOverCanvasGroup;

    public float fadeDuration = 1f;

    const int k_MaxSortingLayer = 32767;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup)
    {
        isFading = true;
        canvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / fadeDuration;
        while (Mathf.Approximately(canvasGroup.alpha, finalAlpha) == false)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        canvasGroup.alpha = finalAlpha;
        isFading = false;
        canvasGroup.blocksRaycasts = false;
    }

    public static void SetAlpha(float alpha)
    {
        Instance.faderCanvasGroup.alpha = alpha;
    }

    public static IEnumerator FadeSceneIn()
    {
        CanvasGroup canvasGroup;
        if (Instance.faderCanvasGroup.alpha > 0.1f)
            canvasGroup = Instance.faderCanvasGroup;
        else
            canvasGroup = Instance.gameOverCanvasGroup;

        yield return Instance.StartCoroutine(Instance.Fade(0, canvasGroup));

        canvasGroup.gameObject.SetActive(false);
    }

    public static IEnumerator FadeSceneOut(FadeType fadeType = FadeType.Black)
    {
        CanvasGroup canvasGroup;
        if (fadeType == FadeType.Black)
            canvasGroup = Instance.faderCanvasGroup;
        else
            canvasGroup = Instance.gameOverCanvasGroup;

        canvasGroup.gameObject.SetActive(true);
        yield return Instance.StartCoroutine(Instance.Fade(1, canvasGroup));
    }
}
