using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private float xCurrentPos;

    public EnemyMoveState(EntityEnemy entityEnemy, StateMachine stateMachine, EnemyData enemyData, string animBoolName) : base(entityEnemy, stateMachine, enemyData, animBoolName)
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xCurrentPos = entityEnemy.gameObject.transform.position.x;

        if (enemyData.isOnFloatStone)
        {
            if (entityEnemy.IsGround() == false)
            {
                if (enemyData.isHasIdleState)
                    stateMachine.ChangeState(entityEnemy.IdleState);
                else
                    entityEnemy.Flip();
            }
        }
        else
        {
            if ((xCurrentPos - entityEnemy.xStartPos) >= enemyData.xDistance && entityEnemy.FacingDirection == 1 ||
                (xCurrentPos - entityEnemy.xStartPos) <= -enemyData.xDistance && entityEnemy.FacingDirection == -1)
            {
                if (enemyData.isHasIdleState)
                    stateMachine.ChangeState(entityEnemy.IdleState);
                else
                    entityEnemy.Flip();
            }
        }

        if (enemyData.isHasAttackState)
        {
            if (entityEnemy.isDetectPlayer)
                stateMachine.ChangeState(entityEnemy.AttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entityEnemy.SetVelocityX(enemyData.moveSpeed * Time.deltaTime * entityEnemy.FacingDirection);
        if (enemyData.isHasAttackState)
        {
            CheckPlayer();
        }

    }

    private void CheckPlayer()
    {

        RaycastHit2D hit =
        Physics2D.Raycast(entityEnemy.EnemyRigid.position, Vector2.right * entityEnemy.FacingDirection, enemyData.distanceDetectPlayer, LayerMask.GetMask("Player"));

        Debug.DrawRay(entityEnemy.EnemyRigid.position, Vector2.right * entityEnemy.FacingDirection * enemyData.distanceDetectPlayer, Color.white);

        if (hit.collider != null)
        {
            entityEnemy.isDetectPlayer = true;
        }

    }
}
