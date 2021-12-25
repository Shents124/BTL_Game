using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    void Start()
    {
        button.onClick.AddListener(PauseGame);
    }

    private void PauseGame()
    {
        GameManager.Instance.PauseGame();
    }
}
