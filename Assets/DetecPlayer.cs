using UnityEngine.Events;
using UnityEngine;

public class DetecPlayer : MonoBehaviour
{
    public UnityEvent OnTrigger;
    public UnityEvent OnExit;
    public GameObject Player;
    public Vector3 playerPos;

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            playerPos = Player.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.gameObject;
            OnTrigger?.Invoke();
           // playerPos = Player.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            Player = null;
            OnExit?.Invoke();
           // playerPos = Player.transform.position;
        }
    }

    public Vector3 GetPlayerPos() => playerPos;
}
