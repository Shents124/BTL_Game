using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(other.GetComponent<Player>().DieRespawnCoroutine());
    }
}
