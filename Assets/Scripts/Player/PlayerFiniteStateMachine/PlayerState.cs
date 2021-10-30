
public class PlayerState : State
{
    protected Player player;
    protected PlayerData playerData;

    public PlayerState()
    {

    }

    public PlayerState(Player player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;
        this.playerData = playerData;
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool(animBoolName, true);
    }

    public override void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
    }

    public override void LogicUpdate()
    {
        if (player.InputHandle.IsDashing())
        {
            stateMachine.ChangeState(player.DashState);
            player.InputHandle.SetDashInputToFalse();
        }
        else if(player.PlayerCombat.GetIsDamaged())
        {
            stateMachine.ChangeState(player.HitState);
        }
    }
}
