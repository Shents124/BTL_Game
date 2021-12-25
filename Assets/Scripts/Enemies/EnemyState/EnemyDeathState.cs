public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EntityEnemy entityEnemy, StateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(entityEnemy, stateMachine, enemyData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        entityEnemy.SetVelocityX(0);
        
        if(enemyData.isHasAttackState == false)
            entityEnemy.GetComponent<EnemyCombat>().enabled = false;
    }
    public override void AnimationFinishTrigger()
    {
        entityEnemy.EnemyCombat.DestroyEnemy();
    }
}
