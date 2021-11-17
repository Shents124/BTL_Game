using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class ActionBoss : Action
{
    protected Rigidbody2D rigidBoss;
    protected Animator animator;
    protected Boss_Combat boss_Combat;
    public override void OnAwake()
    {
        rigidBoss = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        boss_Combat = GetComponent<Boss_Combat>();
    }

    protected void Flip()
    {
        var facingRight = (SharedInt)GlobalVariables.Instance.GetVariable("FacingRight");
        int x = facingRight.Value;
        x *= -1;
        GlobalVariables.Instance.SetVariableValue("FacingRight", x);

        transform.Rotate(new Vector3(0, 180, 0));
    }
}
