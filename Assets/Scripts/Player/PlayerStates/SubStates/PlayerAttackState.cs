using UnityEngine;
public class PlayerAttackState : PlayerAbilityState
{
    private Vector2 input;
    private bool isGround;

    public PlayerAttackState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        if (isAnimationFinished)
        {
            if (input.x != 0)
                stateMachine.ChangeState(player.MoveState);
            else if (input.x == 0)
                stateMachine.ChangeState(player.IdleState);
            else if (isGround == false)
                stateMachine.ChangeState(player.AirState);
        }
    }

    public override void AnimationTrigger()
    {    
        player.PlayerCombat.DoingDamage();
    }
}
