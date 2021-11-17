using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Move variables")]
    public bool isOnFloatStone;
    public float moveSpeed;
    public float xDistance;
    public float groundCheckRadius = 0.15f;
    public LayerMask whatIsGround;

    [Header("Idle variables")]
    public bool isHasIdleState;
    public float idleTime;


    [Header("Health variable")]
    public float maxHealth;

    [Header("Attack variable")]
    public bool isHasAttackState;
    public int dame = 1;
    public float attackRange = 0.15f;
    public float knockBackVelocity = 7f;
    public LayerMask whatIsPlayer;
    public float distanceDetectPlayer;
}
