using UnityEngine;

public class Trap_Sprikes : MonoBehaviour
{
    [SerializeField] private int dame = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerCombat>().GetCurrentHealth() > 1)         
                StartCoroutine(other.gameObject.GetComponent<Player>().DieRespawnCoroutine());
                
            other.gameObject.GetComponent<IDamageable>().TakeDame(dame, Vector3.zero);
        }
    }

}
