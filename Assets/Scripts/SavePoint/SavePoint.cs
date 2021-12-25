using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SavePoint : MonoBehaviour
{
    Animator animator;
    public bool respawnFacingRight;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Reset()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            //set check point
            player.SetSavePoint(this);
            animator.SetTrigger("Saving");
            //save data
        }
    }

}
