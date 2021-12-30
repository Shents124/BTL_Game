
public class PlayerHitState : PlayerState
{
    public PlayerHitState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.PlayerCombat.SetIsDamaged(false);

        if(player.CheckIfGround())
            stateMachine.ChangeState(player.IdleState);
        else
            stateMachine.ChangeState(player.AirState);
    }

}
