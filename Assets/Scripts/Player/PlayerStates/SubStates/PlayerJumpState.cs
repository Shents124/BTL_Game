
public class PlayerJumpState : PlayerAbilityState
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
        isAbilityDone = true;  
    }

    public bool CanJump()
    {
        if (player.amountOfJumpsLeft > 0)
            return true;

        return false;
    }

    public void ResetAmountOfJumpLeft() => player.amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmoutOfJumpLeft() => player.amountOfJumpsLeft--;
}
