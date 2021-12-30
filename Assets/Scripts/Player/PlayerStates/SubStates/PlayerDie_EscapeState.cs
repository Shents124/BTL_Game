
public class PlayerDie_EscapeState : GroundedState
{
    public PlayerDie_EscapeState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
         
        if (isAnimationFinished)
            stateMachine.ChangeState(player.IdleState);
    }
}
