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
    public CanvasGroup gameVictoryCanvasGroup;
    const int k_MaxSortingLayer = 32767;

    protected IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup, float fadeDuration)
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
        float fadeDuration = 1f;
        if (Instance.faderCanvasGroup.alpha > 0.1f)
            canvasGroup = Instance.faderCanvasGroup;
        else
        {
            canvasGroup = Instance.gameOverCanvasGroup;
            fadeDuration = 2f;
        }


        yield return Instance.StartCoroutine(Instance.Fade(0, canvasGroup, fadeDuration));

        canvasGroup.gameObject.SetActive(false);
    }

    public static IEnumerator FadeSceneOut(FadeType fadeType = FadeType.Black)
    {
        CanvasGroup canvasGroup;
        float fadeDuration = 1f;
        if (fadeType == FadeType.Black)
            canvasGroup = Instance.faderCanvasGroup;
        else
        {
            canvasGroup = Instance.gameOverCanvasGroup;
            fadeDuration = 2f;
        }


        canvasGroup.gameObject.SetActive(true);
        yield return Instance.StartCoroutine(Instance.Fade(1, canvasGroup, fadeDuration));
    }

    public static IEnumerator FadeVictoryOut(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instance.gameVictoryCanvasGroup.gameObject.SetActive(true);
        Instance.gameVictoryCanvasGroup.blocksRaycasts = true;
        yield return Instance.StartCoroutine(Instance.Fade(1, Instance.gameVictoryCanvasGroup, 2f));
    }
}
