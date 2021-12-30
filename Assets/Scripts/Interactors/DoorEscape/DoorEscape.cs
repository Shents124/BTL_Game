using UnityEngine;

public class DoorEscape : MonoBehaviour
{
    public PlayerInputHandle playerInputHandle;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInputHandle.canGetInput = false;
            SceneController.Instance.LoadSceneWithName();
        }
    }  
}
