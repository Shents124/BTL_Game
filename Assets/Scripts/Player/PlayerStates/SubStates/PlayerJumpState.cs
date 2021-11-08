
public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
       
    }

    public override void Enter()
    {
        base.Enter();
        player.InstantiateEffect(playerData.jumpEffect);
        player.SetVelocityY(playerData.jumpVelocity);
        DecreaseAmoutOfJumpLeft();
    }

    public bool CanJump()
    {
        if (player.amountOfJumpsLeft > 0)
            return true;

        return false;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

         if (player.CheckIfGround())
                stateMachine.ChangeState(player.IdleState);
            else
                stateMachine.ChangeState(player.AirState);
    }

    public void ResetAmountOfJumpLeft() => player.amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmoutOfJumpLeft() => player.amountOfJumpsLeft--;
}
