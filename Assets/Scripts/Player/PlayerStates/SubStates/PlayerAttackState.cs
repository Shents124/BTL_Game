public class PlayerAttackState : PlayerState
{
    private bool isGround;

    public PlayerAttackState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        if(player.CheckIfGround())
            player.SetVelocityX(0);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGround = player.CheckIfGround();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if(isGround)
                stateMachine.ChangeState(player.IdleState);
            else if (isGround == false)
                stateMachine.ChangeState(player.AirState);
        }
    }

    public override void AnimationTrigger()
    {    
        player.PlayerCombat.DoingDamage();
    }
}
