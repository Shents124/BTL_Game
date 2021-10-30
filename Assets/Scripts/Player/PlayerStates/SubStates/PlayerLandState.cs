public class PlayerLandState : GroundedState
{
    public PlayerLandState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.InstantiateEffect(playerData.landEffect);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (input.x != 0)
            stateMachine.ChangeState(player.MoveState);
        else if (isAnimationFinished)
            stateMachine.ChangeState(player.IdleState);
    }
}
