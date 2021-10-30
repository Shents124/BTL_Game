using UnityEngine;

public class PlayerPushState : GroundedState
{

    public PlayerPushState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        EventBroker.CallOnPushing();
    }

    public override void Exit()
    {
        base.Exit();
        EventBroker.CallOnStopPushing();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPushing == false || Mathf.Abs(input.x) <= 0.01f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityX(playerData.pushVelocity * input.normalized.x * Time.deltaTime);
    }
}
