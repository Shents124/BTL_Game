using UnityEngine;

public class DoorSpike : MonoBehaviour
{
    private Animator animator;
    public bool isOpen = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        if(isOpen)
            Open();
    }
    public void Open() => animator.SetBool("isOpen", true);
    public void Close() => animator.SetBool("isOpen", false);
}
