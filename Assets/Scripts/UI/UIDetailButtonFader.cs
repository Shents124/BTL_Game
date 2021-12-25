using System.Collections;
using UnityEngine;

public class UIDetailButtonFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float faderDuation = 1f;

    IEnumerator Fade(float finalAlpha)
    {
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / faderDuation;
        while (Mathf.Approximately(canvasGroup.alpha, finalAlpha) == false)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = finalAlpha;
    }

    IEnumerator FadeInAndFadeOut()
    {
        yield return StartCoroutine(Fade(1));
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Fade(0));
    }

    public void DisplayDetailButton() => StartCoroutine("FadeInAndFadeOut");

}
