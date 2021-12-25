using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class DestroyBoss : ActionBoss
{
    public float waitTimeDestroy;

    private bool isDestroyed;

    public override void OnStart()
    {
        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(waitTimeDestroy);
        Object.Destroy(gameObject);
        isDestroyed = true;
    }

    public override TaskStatus OnUpdate()
    {
        return isDestroyed ? TaskStatus.Success : TaskStatus.Failure;
    }
}
