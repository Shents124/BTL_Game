using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable
{
    [Header("Health Variables")]
    [SerializeField] private FloatVariable currentHealth;
    [SerializeField] private FloatVariable maxHealth;

    [Header("Attack Variables")]
    [SerializeField] private Transform attackPointPos;
    [SerializeField] private PlayerData playerData;

    private bool isDamaged;
    private bool isDeath;

    private void Start()
    {
        if (currentHealth.value <= 0)
            currentHealth.value = maxHealth.value;

        isDamaged = false;
        isDeath = false;
    }

    public void DoingDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointPos.position, playerData.attackRange, playerData.whatIsEnemy);

        if (hitEnemies.Length > 0)
        {
            Instantiate(playerData.hitEffect, attackPointPos.position, Quaternion.identity);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<IDamageable>().TakeDame(playerData.dame);
            }
        }
    }

    public void TakeDame(int amountOfDame)
    {
        currentHealth.value -= amountOfDame;
        isDamaged = true;

        if(currentHealth.value <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isDeath = true;
    }

    public bool GetIsDamaged() => isDamaged;
    public void SetIsDamaged(bool value) => isDamaged = value;

}
