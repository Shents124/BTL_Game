public class EnemyHitState : EnemyState
{
    private float lastVelocity;
    public EnemyHitState(EntityEnemy entityEnemy, StateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(entityEnemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //lastVelocity = entityEnemy.EnemyRigid.velocity.x;
        //entityEnemy.SetVelocityX(0);
        
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        entityEnemy.EnemyCombat.IsDamaged = false;
        stateMachine.ChangeState(entityEnemy.MoveState);
    }
}
