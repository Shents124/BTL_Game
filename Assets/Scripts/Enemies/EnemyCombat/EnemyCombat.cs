using UnityEngine;

public class EnemyCombat : MonoBehaviour, IDamageable, IKnockbackable
{
    public GameObject hitEffect;
    public bool IsDamaged { get; set; }
    public bool IsDeath { get; private set; }
    [SerializeField] private EnemyData enemyData;

    private EntityEnemy entityEnemy;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyData.maxHealth;
        entityEnemy = GetComponent<EntityEnemy>();
        IsDeath = false;
        IsDamaged = false;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamageable>().TakeDame(enemyData.dame, Vector3.zero);
            other.gameObject.GetComponent<IKnockbackable>().KnockBack(enemyData.knockBackVelocity);
        }
    }

    public void TakeDame(int amountOfDame, Vector3 damePos)
    {
        Instantiate(hitEffect, damePos, Quaternion.identity);
        currentHealth -= amountOfDame;
        IsDamaged = true;

        if (currentHealth <= 0)
        {
            IsDeath = true;
        }
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    public void KnockBack(float velocity)
    {
        entityEnemy.SetVelocityX(velocity);
    }
}
