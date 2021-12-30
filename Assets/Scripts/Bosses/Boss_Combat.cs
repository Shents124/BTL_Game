using UnityEngine;

public class Boss_Combat : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public GameObject hitEffect;
    [SerializeField] private FloatVariable currentHealth;

    private void OnEnable()
    {
        currentHealth.value = maxHealth;
    }

    public void TakeDame(int amountOfDame, Vector3 damePos)
    {
        if (currentHealth.value > 0)
        {
            currentHealth.value -= amountOfDame;
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

    }

    public float GetCurrentHealth() => currentHealth.value;
}
