using UnityEngine;

public class Boss_Combat : MonoBehaviour, IDamageable
{
    public int maxHealth;
    [SerializeField] private FloatVariable currentHealth;

    private void OnEnable()
    {
        currentHealth.value = maxHealth;
    }
    
    public void TakeDame(int amountOfDame, Vector3 damePos)
    {
        currentHealth.value -= amountOfDame;
    }

    public float GetCurrentHealth() => currentHealth.value;
}
