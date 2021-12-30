using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SavePoint : MonoBehaviour
{
    Animator animator;
    public bool respawnFacingRight;
    public RandomAudioPlayer savingAudio;
    public bool isSavePos = true;
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
            player.SetSavePoint(this);
            animator.SetTrigger("Saving");
            savingAudio.PlayRandomSound();
            
            if(isSavePos)
                EventBroker.CallOnSavePlayerPos(this);
        }
    }

}
