using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class Move : ActionBoss
{
    public SharedFloat _speed;
    public Transform[] targets;
    public bool firstMove = false;
    private Transform target;


    // Start is called before the first frame update
    public override void OnStart()
    {
        base.OnStart();
        animator.SetBool("isFlight", true);

        GetTargetPos();

        CheckFlip();
    }

    public override TaskStatus OnUpdate()
    {
        if (rigidBoss == null)
        {
            Debug.LogWarning("Rigidbody2D is null");
            return TaskStatus.Failure;
        }


        if (Vector3.SqrMagnitude(transform.position - target.position) < 0.5)
            return TaskStatus.Success;

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed.Value * Time.deltaTime);

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        animator.SetBool("isFlight", false);
    }

    private void GetTargetPos()
    {
        int index;
        if (firstMove == true)
        {
            index = 0;
        }
        else
        {
            var indexValue = (SharedInt)GlobalVariables.Instance.GetVariable("index");
            int lastIndex = indexValue.Value;

            SwapTargets(lastIndex);

            index = Random.Range(0, targets.Length - 1);
            
        }

        target = targets[index];
        GlobalVariables.Instance.SetVariableValue("index", index);
    }

    private void SwapTargets(int targetIndex)
    {
        Transform currentTarget = targets[targetIndex];
        targets[targetIndex] = targets[targets.Length - 1];
        targets[targets.Length - 1] = currentTarget;
    }

    private void CheckFlip()
    {
        float distance = transform.position.x - target.position.x;
        var facingRight = (SharedInt)GlobalVariables.Instance.GetVariable("FacingRight");

        if (distance > 0 && facingRight.Value == 1 || distance < 0 && facingRight.Value == -1)
            Flip();
    }
}
