using UnityEngine;

public class PlayerMoveState : GroundedState
{
    public PlayerMoveState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckIfShouldFlip(input.x);

        if (Mathf.Abs(input.x) <= 0.1f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (isPushing == true)
        {
            stateMachine.ChangeState(player.PushState);
        }
        else if (player.InputHandle.IsAttacking())
        {
            stateMachine.ChangeState(player.AttackState);
            player.InputHandle.SetAttackInputToFalse();

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityX(playerData.movementVelocity * input.normalized.x * Time.deltaTime);
    }
}
