using UnityEngine;
using System.Collections;

public class PlayerDashState : PlayerState
{
    private Vector2 lastAfterImagePos;
    private int amountOfDashLeft;
    public PlayerDashState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfDashLeft = playerData.amountOfDash;
    }

    public override void Enter()
    {
        base.Enter();
        player.InstantiateEffect(playerData.jumpEffect);
        player.PlayDashingSound();
        player.SetGravity(0);
        PlaceAfterimage();
        amountOfDashLeft--;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetGravity(playerData.defaultGravityScale);
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

    public bool CanDash()
    {
        if (amountOfDashLeft > 0) return true;
        return false;
    }

    public void ResetAmountOfDashLeft() => amountOfDashLeft = playerData.amountOfDash;

    public IEnumerator GroundDashDelay()
    {
        yield return new WaitForSeconds(playerData.dashCoolDown);
        ResetAmountOfDashLeft();
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAfterImagePos) >= playerData.distanceBetweenImages)
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
