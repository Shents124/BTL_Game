
public class PlayerLandState : GroundedState
{
    public PlayerLandState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.InstantiateEffect(playerData.landEffect);
        player.PlayLandingSound();
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
         
        if (isAnimationFinished)
            stateMachine.ChangeState(player.IdleState);
    }
}
