using UnityEngine;
public class PlayerAirState : PlayerState
{
    private const string YVelocity = "yVelocity";
    private bool isGround;
    private Vector2 input;
    public PlayerAirState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGround = player.CheckIfGround();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        input = player.InputHandle.GetMove();

        if (isGround && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
            player.PlayerRigid.gravityScale = playerData.defaultGravityScale;
        }
        else if (player.InputHandle.IsJumping() && player.amountOfJumpsLeft > 0)
        {
            stateMachine.ChangeState(player.JumpState);
            player.InputHandle.SetJumpInputToFalse();
        }
        else if (player.InputHandle.IsAttacking())
        {
            stateMachine.ChangeState(player.AttackState);
            player.InputHandle.SetAttackInputToFalse();
        }
        else if (player.InputHandle.IsDashing() && player.DashState.CanDash())
        {
            stateMachine.ChangeState(player.DashState);
            player.InputHandle.SetDashInputToFalse();
        }

        player.CheckIfShouldFlip(input.x);
        player.Anim.SetFloat(YVelocity, player.CurrentVelocity.y);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isGround == false)
        {
            player.SetVelocityX(playerData.movementVelocity * input.normalized.x * Time.deltaTime);
        }

        if (player.PlayerRigid.velocity.y < 0)
            player.PlayerRigid.gravityScale += player.PlayerRigid.gravityScale * Time.deltaTime;
        else
            player.PlayerRigid.gravityScale = playerData.defaultGravityScale;
    }

}
