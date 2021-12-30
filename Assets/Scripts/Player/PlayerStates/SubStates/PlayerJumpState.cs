public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.InstantiateEffect(playerData.jumpEffect);
        player.PlayJumpingSound();
        player.SetVelocityY(playerData.jumpVelocity);
        DecreaseAmoutOfJumpLeft();
 
        stateMachine.ChangeState(player.AirState);
    }

    public void ResetAmountOfJumpLeft() => player.amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmoutOfJumpLeft() => player.amountOfJumpsLeft--;
}
