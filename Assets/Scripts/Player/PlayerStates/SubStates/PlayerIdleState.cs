using UnityEngine;

public class PlayerIdleState : GroundedState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }
    
    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs(input.x) > 0.1f)
        {
            if (isPushing == false)
                stateMachine.ChangeState(player.MoveState);
            else
                stateMachine.ChangeState(player.PushState);
        }
    }
}
