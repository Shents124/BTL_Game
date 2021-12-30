using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputHandle playerInputHandle;

    public void DisableInput() => playerInputHandle.DisableInput();
    public void EnableInput() => playerInputHandle.EnableInput();
}
