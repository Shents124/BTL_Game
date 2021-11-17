
using UnityEngine;

public class DetecPlayer : MonoBehaviour
{
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
           // playerPos = Player.transform.position;
        }
    }

    public Vector3 GetPlayerPos() => playerPos;
}
