using System.Collections;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class DestroyBoss : ActionBoss
{
    public SharedBool isDeath;
    public SharedFloat speed;
    public float waitTimeDestroy;
    public RandomAudioPlayer deathAudio;
    public GameObject jinn_Boss;

    private bool isDestroyed;
    public override void OnStart()
    {
        speed.Value = 0;
        StartCoroutine("Destroy");
        isDeath.Value = true;
    }

    IEnumerator Destroy()
    {
        animator.SetTrigger("Death");
        EventBroker.CallOnBossDead();
        yield return new WaitForSeconds(waitTimeDestroy);
        deathAudio.PlayRandomSound();
        jinn_Boss.SetActive(false);
        GetComponent<BehaviorTree>().enabled = false;
        StartCoroutine(ScreenFader.FadeVictoryOut(1));
        isDestroyed = true;
    }

    public override TaskStatus OnUpdate()
    {
        return isDestroyed ? TaskStatus.Success : TaskStatus.Failure;
    }
}
