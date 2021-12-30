using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;
using System.Collections;

public class SpawnFallingFires : ActionBoss
{
    public SharedBool isDeath;
    public Collider2D spawnAreaCollider;
    public GameObject firePrefab;
    public float spawnInterval = 0.5f;
    public float waitTime;
    public RandomAudioPlayer EarthquakeSound;
    private int spawnCount;
  
    public override void OnStart()
    {
        spawnCount = Random.Range(10, 20);
        waitTime = spawnInterval * spawnCount;
        ShakeCamera.Instance.Shake(waitTime);
        EarthquakeSound.PlayRandomSound();
        
        StartCoroutine(Spawn());
    }
    public override TaskStatus OnUpdate()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            EarthquakeSound.Stop();
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    IEnumerator Spawn()
    {
        spawnCount = Random.Range(10, 20);
        for (int i = 0; i < spawnCount; i++)
        {
            if (isDeath.Value == false)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnFire();
            }

        }
        yield return null;
    }

    private void SpawnFire()
    {
        var randomX = Random.Range(spawnAreaCollider.bounds.min.x, spawnAreaCollider.bounds.max.x);
        var fire = Object.Instantiate(firePrefab, new Vector3(randomX, spawnAreaCollider.bounds.min.y), Quaternion.identity);
    }
}
