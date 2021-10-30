using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Move variables")]
    [SerializeField] public bool isOnFloatStone;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float xDistance;
    [SerializeField] public float groundCheckRadius = 0.15f;
    [SerializeField] public LayerMask whatIsGround;

    [Header("Idle variables")]
    [SerializeField] public bool isHasIdleState;
    [SerializeField] public float idleTime;


    [Header("Health variable")]
    [SerializeField] public float maxHealth;

    [Header("Attack variable")]
    [SerializeField] public bool isHasAttackState;
    [SerializeField] public int dame = 1;
    [SerializeField] public float attackRange = 0.15f;
    [SerializeField] public LayerMask whatIsPlayer;
    [SerializeField] public float distanceDetectPlayer;

}
