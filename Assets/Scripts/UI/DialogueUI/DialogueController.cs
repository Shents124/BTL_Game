using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public DialoguePhrases dialoguePhrases;
    public Animator animator;

    public TextMeshProUGUI textMeshProUGUI;

    private Coroutine deactivationCoroutine;

    private readonly int hasActivePara = Animator.StringToHash("Active");

    IEnumerator SetAnimationParamaterWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(hasActivePara, false);
    }

    public void ActiveCanvasWithText(string text)
    {
        if (deactivationCoroutine != null)
        {
            StopCoroutine(deactivationCoroutine);
            deactivationCoroutine = null;
        }

        gameObject.SetActive(true);
        animator.SetBool(hasActivePara, true);
        textMeshProUGUI.text = text;
    }

    public void ActiveCanvasWithPhrase(string phraseKey)
    {
        if (deactivationCoroutine != null)
        {
            StopCoroutine(deactivationCoroutine);
            deactivationCoroutine = null;
        }

        gameObject.SetActive(true);
        animator.SetBool(hasActivePara, true);
        textMeshProUGUI.text = dialoguePhrases.GetValueBykey(phraseKey);
    }

    public void DeactiveCanvasWithDelay(float delay)
    {
        deactivationCoroutine = StartCoroutine(SetAnimationParamaterWithDelay(delay));
    }
}
