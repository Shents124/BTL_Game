using UnityEngine;

public class GroundedState : PlayerState
{
    protected Vector2 input;
    protected bool isPushing;
    private bool isGrounded;

    public GroundedState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGround();
        isPushing = player.CheckIfPush();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumpLeft();
        player.InputHandle.SetJumpInputToFalse();

        player.DashState.ResetAmountOfDashLeft();
        player.InputHandle.SetDashInputToFalse();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputHandle.GetMove();

        if (player.InputHandle.IsJumping())
        {
            stateMachine.ChangeState(player.JumpState);
            player.InputHandle.SetJumpInputToFalse();
        }
        else if (isGrounded == false)
        {
            stateMachine.ChangeState(player.AirState);
        }
        else if (player.InputHandle.IsDashing() && player.DashState.CanDash())
        {
            stateMachine.ChangeState(player.DashState);
            player.InputHandle.SetDashInputToFalse();
        }
    }
}
