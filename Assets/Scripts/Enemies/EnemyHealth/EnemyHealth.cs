using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsDamaged { get; set; }
    public bool IsDeath { get; private set; }
    [SerializeField] private EnemyData enemyData;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyData.maxHealth;
        IsDeath = false;
        IsDamaged = false;
    }

    public void TakeDame(int amountOfDame)
    {
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
}
