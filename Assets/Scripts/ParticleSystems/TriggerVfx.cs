using UnityEngine;

public class TriggerVfx : MonoBehaviour
{
    public GameObject explonsion;
    private void OnEnable()
    {
        Destroy(this.gameObject, 3f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(explonsion, transform.position, Quaternion.identity);
        Destroy(this.gameObject, 0.1f);
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerCombat>().TakeDame(1, Vector3.zero);
    }
}
