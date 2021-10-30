using UnityEngine;

public class EnemyDamageable : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<IDamageable>().TakeDame(enemyData.dame);
    }
}
