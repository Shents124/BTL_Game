using System.Collections;
using UnityEngine;

public class GameVictory : MonoBehaviour
{
    public GameObject theEndText;
    public GameObject credits;
    public float creditsTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayCredits(creditsTime));
    }

    IEnumerator PlayCredits(float creditsTime)
    {
        yield return new WaitForSeconds(4.5f);
        theEndText.SetActive(false);
        credits.SetActive(true);
        yield return new WaitForSeconds(creditsTime);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("StartMenu");
    }

}
