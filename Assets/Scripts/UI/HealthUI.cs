using System.Collections;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public FloatVariable maxHealth;
    public FloatVariable currentHealth;
    public GameObject healthIconPrefab;

    private Animator[] healthIconAnimators;
    private readonly int m_HashActivePara = Animator.StringToHash("Active");
    private readonly int m_HashInactiveState = Animator.StringToHash("Inactive");
    private const float k_HeartIconAnchorWidth = 0.065f;

    IEnumerator Start()
    {
        yield return null;
        healthIconAnimators = new Animator[maxHealth.value];

        for (int i = 0; i < maxHealth.value; i++)
        {
            GameObject healthIcon = Instantiate(healthIconPrefab);
            healthIcon.transform.SetParent(transform);
            RectTransform healthIconRect = healthIcon.transform as RectTransform;
            healthIconRect.anchoredPosition = Vector2.zero;
            healthIconRect.sizeDelta = Vector2.zero;
            healthIconRect.anchorMin += new Vector2(k_HeartIconAnchorWidth, 0f) * i;
            healthIconRect.anchorMax += new Vector2(k_HeartIconAnchorWidth, 0f) * i;
            healthIconAnimators[i] = healthIcon.GetComponent<Animator>();

            if (currentHealth.value < i + 1)
            {
                healthIconAnimators[i].Play(m_HashInactiveState);
                healthIconAnimators[i].SetBool(m_HashActivePara, false);
            }

        }
    }

    public void ChangeHitPointUI(FloatVariable currentHealth)
    {
        for (int i = 0; i < healthIconAnimators.Length; i++)
            healthIconAnimators[i].SetBool(m_HashActivePara, currentHealth.value >= i + 1);
    }
}
