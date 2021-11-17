using UnityEngine;

public class EntityEnemy : MonoBehaviour
{
    #region Animation variables
    private const string Walk = "walk";
    private const string Hit = "hit";
    private const string Death = "death";
    private const string Idle = "idle";
    private const string Attack = "attack";
    #endregion

    #region State 
    public StateMachine StateMachine { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    public EnemyHitState HitState { get; private set; }
    public EnemyDeathState DeathState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    #endregion

    #region Componets Varibles
    public Animator Anim { get; private set; }
    public Rigidbody2D EnemyRigid { get; private set; }
    public EnemyCombat EnemyCombat { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    #endregion

    #region Other variables
    public bool isDetectPlayer;

    [SerializeField] protected Transform groundCheckPos;
    [SerializeField] protected Transform attackPos;
    [SerializeField] protected EnemyData enemyData;
    private Vector2 workspace;
    private float xPos;
    #endregion

    protected virtual void Awake()
    {
        StateMachine = new StateMachine();
        MoveState = new EnemyMoveState(this, StateMachine, enemyData, Walk);

        HitState = new EnemyHitState(this, StateMachine, enemyData, Hit);
        DeathState = new EnemyDeathState(this, StateMachine, enemyData, Death);

        if (enemyData.isHasIdleState)
            IdleState = new EnemyIdleState(this, StateMachine, enemyData, Idle);

        if (enemyData.isHasAttackState)
            AttackState = new EnemyAttackState(this, StateMachine, enemyData, Attack);
    }
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        EnemyRigid = GetComponent<Rigidbody2D>();
        EnemyCombat = GetComponent<EnemyCombat>();
        StateMachine.Initialize(MoveState);
        FacingDirection = 1;
        xPos = transform.position.x;
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Set Velocity
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        EnemyRigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float jumpVelocity)
    {
        workspace.Set(CurrentVelocity.x, jumpVelocity);
        EnemyRigid.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, enemyData.groundCheckRadius, enemyData.whatIsGround);
    }

    public void Flip()
    {
        FacingDirection *= -1;
        Vector3 _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }

    public float GetXStartPos() => xPos;
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void OnDrawGizmos()
    {
        if (enemyData.isHasAttackState)

            Gizmos.DrawWireSphere(attackPos.position, enemyData.attackRange);
    }

    public void DoingDamage()
    {
        if (enemyData.isHasAttackState)
        {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPos.position, enemyData.attackRange, enemyData.whatIsPlayer);

            if (hitPlayer.Length > 0)
            {
                foreach (Collider2D player in hitPlayer)
                {
                    player.GetComponent<IDamageable>().TakeDame(enemyData.dame);
                }
            }
        }
    }

}
