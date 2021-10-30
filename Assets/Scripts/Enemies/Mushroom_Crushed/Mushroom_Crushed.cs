using UnityEngine;

public class Mushroom_Crushed : MonoBehaviour
{
    [SerializeField] private float velocity = 45f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Fly(velocity);
        }
    }
}
