using UnityEngine;

public class Orb : MonoBehaviour
{
    public GameObject orbCollected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(orbCollected, transform.position, Quaternion.identity);
        Destroy(gameObject); 
    }
}
