
public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EntityEnemy entityEnemy, StateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(entityEnemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entityEnemy.SetVelocityX(0);
    }
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        entityEnemy.DoingDamage();
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        entityEnemy.isDetectPlayer = false;
        stateMachine.ChangeState(entityEnemy.MoveState);
    }

}
