using UnityEngine;

public class PlayerDashState : PlayerState
{
    private Vector2 lastAfterImagePos;
    public PlayerDashState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.InstantiateEffect(playerData.jumpEffect);
        player.SetGravity(0);
        PlaceAfterimage();
    }

    public override void Exit()
    {
        base.Exit();
        player.SetGravity(3);
    }

    public override void LogicUpdate()
    {

        CheckIfShouldPlaceAfterImage();

        if (Time.time >= playerData.dashCoolDown + startTime)
        {
            if (player.CheckIfGround())
                stateMachine.ChangeState(player.IdleState);
            else
                stateMachine.ChangeState(player.AirState);          
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

         player.SetVelocityX(playerData.dashVelocity * player.FacingDirection * Time.deltaTime);
         player.SetVelocityY(0);     
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(player.transform.position, lastAfterImagePos) >= playerData.distanceBetweenImages)
        {
            PlaceAfterimage();
        }
    }

    private void PlaceAfterimage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAfterImagePos = player.transform.position;
    }
}
