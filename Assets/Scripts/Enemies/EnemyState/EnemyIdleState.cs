using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EntityEnemy entityEnemy, StateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(entityEnemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entityEnemy.SetVelocityX(0);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if( (Time.time - startTime ) >= enemyData.idleTime)
        {
            entityEnemy.Flip();
            stateMachine.ChangeState(entityEnemy.MoveState);
        }
    }
}
