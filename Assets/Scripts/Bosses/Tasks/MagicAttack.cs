using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class MagicAttack : ActionBoss
{
    public GameObject magicPrefab;
    public Transform attackPos;
    public float attackDelay = 0.2f;
    public DetecPlayer detecPlayer;
    private Vector3 target;

    private bool isDone;
    public override void OnStart()
    {
        base.OnStart();
        isDone = false;
        animator.SetTrigger("Attack");
        target = detecPlayer.GetPlayerPos();

        CheckFlip();

        StartCoroutine("Attack");
    }

    public override TaskStatus OnUpdate()
    {
        return isDone ? TaskStatus.Success : TaskStatus.Running;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
        var magic = Object.Instantiate(magicPrefab, attackPos.position, Quaternion.identity);
        magic.GetComponent<MagicProjectile>().dir = (target - magic.transform.position).normalized;
        isDone = true;
    }

    private void CheckFlip()
    {
        float distance = transform.position.x - target.x;

        var facingRight = (SharedInt)GlobalVariables.Instance.GetVariable("FacingRight");

        if (distance > 0 && facingRight.Value == 1 || distance < 0 && facingRight.Value == -1)
            Flip();
    }
    public override void OnEnd()
    {
        isDone = false;
    }
}
