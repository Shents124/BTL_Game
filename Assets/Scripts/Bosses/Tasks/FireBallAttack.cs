using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class FireBallAttack : ActionBoss
{
    public GameObject fireBallPrefab;
    public Transform attackPos;
    public float attackDelay = 0.2f;
    public float fireBallSpeed = 5f;
    public SharedInt attackDirection;

    private bool isDone;
   
    public override void OnStart()
    {
        base.OnStart();
        isDone = false;
        animator.SetTrigger("Attack");
        StartCoroutine("Attack");
    }

    public override TaskStatus OnUpdate()
    {
        return isDone ? TaskStatus.Success : TaskStatus.Running;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        var fireBall = Object.Instantiate(fireBallPrefab, attackPos.position, Quaternion.identity);
        fireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(fireBallSpeed * attackDirection.Value, 0);
        isDone = true;
    }

    public override void OnEnd()
    {
        isDone = false;
    }
}
