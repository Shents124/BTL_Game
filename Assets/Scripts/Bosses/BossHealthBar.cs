using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealthBar : MonoBehaviour
{
    public FloatVariable bossCurrentHealth;
    private Slider slider;

    private void OnEnable()
    {
        EventBroker.OnBossDead += DisableHealthBar;
        StartCoroutine(DisplayHeathBar());
    }
    private void OnDisable()
    {
        EventBroker.OnBossDead -= DisableHealthBar;
    }
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = bossCurrentHealth.value;
    }

    public IEnumerator DisplayHeathBar()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(true);
    }
    public void DisableHealthBar() => gameObject.SetActive(false);
}
