using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;
using DG.Tweening;


public class SpawnFallingFires : ActionBoss
{
    public Collider2D spawnAreaCollider;
    public GameObject firePrefab;
    public float spawnInterval = 0.5f;
    public float waitTime;

    private int spawnCount;

    public override void OnStart()
    {
        spawnCount = Random.Range(10, 20);
        waitTime = spawnInterval * spawnCount;
        ShakeCamera.Instance.Shake(waitTime);

        var sequence = DOTween.Sequence();

        for (int i = 0; i < spawnCount; i++)
        {
            sequence.AppendCallback(SpawnFire);
            sequence.AppendInterval(spawnInterval);
        }
    }
    public override TaskStatus OnUpdate()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void SpawnFire()
    {
        var randomX = Random.Range(spawnAreaCollider.bounds.min.x, spawnAreaCollider.bounds.max.x);
        var fire = Object.Instantiate(firePrefab, new Vector3(randomX, spawnAreaCollider.bounds.min.y), Quaternion.identity);
    }
}
