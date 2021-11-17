using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable, IKnockbackable
{
    [Header("Health Variables")]
    [SerializeField] private FloatVariable currentHealth;
    [SerializeField] private FloatVariable maxHealth;

    [Header("Attack Variables")]
    [SerializeField] private Transform attackPointPos;
    [SerializeField] private PlayerData playerData;

    private Player player;
    private bool isDamaged;
    private bool isDeath;

    private void Start()
    {
        if (currentHealth.value <= 0)
            currentHealth.value = maxHealth.value;

        player = GetComponent<Player>();

        isDamaged = false;
        isDeath = false;
    }

    public void DoingDamage()
    {
        Collider2D[] hitEnemies =
        Physics2D.OverlapCapsuleAll(attackPointPos.position, playerData.attackSize, CapsuleDirection2D.Horizontal, playerData.angle, playerData.whatIsEnemy);

        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                Instantiate(playerData.hitEffect, attackPointPos.transform.position, Quaternion.identity);
                enemy.GetComponent<IDamageable>().TakeDame(playerData.dame);
                //enemy.GetComponent<IKnockbackable>().KnockBack(player.FacingDirection * playerData.knockBackVelocity);
            }
        }
    }

    public void TakeDame(int amountOfDame)
    {
        currentHealth.value -= amountOfDame;
        isDamaged = true;

        if (currentHealth.value <= 0)
        {
            Death();
        }
    }

    private void Death() => isDeath = true;
    public bool GetIsDamaged() => isDamaged;
    public void SetIsDamaged(bool value) => isDamaged = value;

    public void KnockBack(float velocity)
    {
        player.SetVelocityX(velocity * -player.FacingDirection);
    } 
}
