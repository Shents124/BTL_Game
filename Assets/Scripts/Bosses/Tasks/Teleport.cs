using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Teleport : ActionBoss
{
    public Transform[] teleportPostions;
    public SharedInt facingDirection;
    public float timeAnimationDone = 0.3f;

    private int index;
    private bool isDone;

    public override void OnStart()
    {
        isDone = false;
        StartCoroutine("Disappear");
    }

    public override TaskStatus OnUpdate()
    {
        return isDone ? TaskStatus.Success : TaskStatus.Running;
    }
    IEnumerator Disappear()
    {
        animator.SetTrigger("Disappear");
        yield return new WaitForSeconds(timeAnimationDone);
        StartCoroutine("Appear");
    }

    IEnumerator Appear()
    {
        ChangePosition();
        animator.SetTrigger("Appear");
        CheckFlip();
        yield return new WaitForSeconds(timeAnimationDone + 0.5f);
        isDone = true;
    }

    private void ChangePosition()
    {
        index = Random.Range(0, teleportPostions.Length);
        SetFacingDirection(index);
        this.transform.position = teleportPostions[index].position;
    }

    private void SetFacingDirection(int index)
    {
        if (index == 0)
            facingDirection.Value = 1;
        else
            facingDirection.Value = -1;
    }

    private void CheckFlip()
    {
        
        var facingRight = (SharedInt)GlobalVariables.Instance.GetVariable("FacingRight");

        if (index > 0 && facingRight.Value == 1 || index <= 0 && facingRight.Value == -1)
            Flip();
    }
}
