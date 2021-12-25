using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger2DEvent : MonoBehaviour
{
    public UnityEvent onEnter, onExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            onEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            onExit?.Invoke();
    }
}
