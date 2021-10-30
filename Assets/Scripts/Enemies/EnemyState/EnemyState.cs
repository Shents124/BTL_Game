
public class EnemyState : State
{
    protected EntityEnemy entityEnemy;
    protected EnemyData enemyData;

    public EnemyState() { }

    public EnemyState(EntityEnemy entityEnemy, StateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.entityEnemy = entityEnemy;
        this.enemyData = enemyData;
    }

    public override void Enter()
    {
        base.Enter();
        entityEnemy.Anim.SetBool(animBoolName, true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (entityEnemy.EnemyHealth.IsDeath)
        {
            stateMachine.ChangeState(entityEnemy.DeathState);
        }
        else if (entityEnemy.EnemyHealth.IsDamaged && entityEnemy.EnemyHealth.IsDeath == false)
        {
            stateMachine.ChangeState(entityEnemy.HitState);
        }
    }

    public override void Exit()
    {
        entityEnemy.Anim.SetBool(animBoolName, false);
    }
}
