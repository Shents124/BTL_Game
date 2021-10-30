using UnityEngine;

public class Player : MonoBehaviour
{
    #region Animaion variables
    private const string Idle = "idle";
    private const string Move = "move";
    private const string InAir = "inAir";
    private const string Land = "land";
    private const string Attack = "attack";
    private const string Push = "push";
    private const string Die_Escape = "die_escape";
    private const string Dash = "dash";
    private const string Hit = "hit";
    #endregion

    #region State Variables
    public StateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerPushState PushState { get; private set; }
    public PlayerDie_EscapeState Die_EscapeState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerHitState HitState { get; private set; }

    [HideInInspector]
    public int amountOfJumpsLeft;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D PlayerRigid { get; private set; }
    public PlayerInputHandle InputHandle { get; private set; }
    public PlayerCombat PlayerCombat { get; private set; }
    private CapsuleCollider2D capsuleCollider2D;
    #endregion

    #region Others variables

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }
    public bool IsEscaspe { get; private set; }
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform pushCheck;
    [SerializeField] private Transform jumpEffectPos;
    [SerializeField] PhysicsMaterial2D bounci;
    [SerializeField] private PlayerData playerData;
    private Vector2 workspace;
    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, Idle);
        MoveState = new PlayerMoveState(this, StateMachine, playerData, Move);
        JumpState = new PlayerJumpState(this, StateMachine, playerData, InAir);
        AirState = new PlayerAirState(this, StateMachine, playerData, InAir);
        LandState = new PlayerLandState(this, StateMachine, playerData, Land);
        AttackState = new PlayerAttackState(this, StateMachine, playerData, Attack);
        PushState = new PlayerPushState(this, StateMachine, playerData, Push);
        Die_EscapeState = new PlayerDie_EscapeState(this, StateMachine, playerData, Die_Escape);
        DashState = new PlayerDashState(this, StateMachine, playerData, Dash);
        HitState = new PlayerHitState(this, StateMachine, playerData, Hit);

        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandle = GetComponent<PlayerInputHandle>();
        PlayerRigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        PlayerCombat = GetComponent<PlayerCombat>();
        StateMachine.Initialize(IdleState);
        FacingDirection = 1;
        IsEscaspe = false;
    }

    private void Update()
    {
        CurrentVelocity = PlayerRigid.velocity;
        StateMachine.CurrentState.LogicUpdate();

        if (IsOnWall())
            Slide();
        else
            capsuleCollider2D.sharedMaterial = null;

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        IsOnWall();
    }

    #region Set Velocity
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        PlayerRigid.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float jumpVelocity)
    {
        workspace.Set(CurrentVelocity.x, jumpVelocity);
        PlayerRigid.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Method
    public bool CheckIfGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfPush()
    {
        return Physics2D.OverlapCircle(pushCheck.position, playerData.groundCheckRadius, playerData.whatIsPush);
    }

    public bool IsOnWall()
    {
        return Physics2D.OverlapCircle(pushCheck.position, playerData.groundCheckRadius, playerData.WhatIsWall);
    }

    public void CheckIfShouldFlip(float xInput)
    {
        if (FacingDirection == 1 && xInput < 0 ||
            FacingDirection == -1 && xInput > 0)
            Flip();
    }

    #endregion

    private void Slide() => capsuleCollider2D.sharedMaterial = bounci;
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        Vector3 _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }
    public void SetGravity(int gravityScale) => PlayerRigid.gravityScale = gravityScale;
    public void Escape() => IsEscaspe = true;
    public void InstantiateEffect(GameObject effect) => Instantiate(effect, jumpEffectPos.position, Quaternion.identity);
    public void Fly(float velocity)
    {
        PlayerRigid.velocity = new Vector2(0, velocity);
        StateMachine.ChangeState(AirState);
        InstantiateEffect(playerData.jumpEffect);
        amountOfJumpsLeft = playerData.amountOfJumps - 1;
    }
}
