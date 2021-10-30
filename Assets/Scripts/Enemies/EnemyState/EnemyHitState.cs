public class EnemyHitState : EnemyState
{
    private float lastVelocity;
    public EnemyHitState(EntityEnemy entityEnemy, StateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(entityEnemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        lastVelocity = entityEnemy.EnemyRigid.velocity.x;
       // entityEnemy.SetVelocityX(0);
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        entityEnemy.EnemyHealth.IsDamaged = false;
        stateMachine.ChangeState(entityEnemy.MoveState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (entityEnemy.EnemyHealth.IsDamaged)
        {
            stateMachine.ChangeState(entityEnemy.HitState);
        }
    }


}
