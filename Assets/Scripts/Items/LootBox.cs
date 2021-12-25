using UnityEngine;

public class LootBox : MonoBehaviour
{
    public Sprite opened;

    private Animator animator;
    private int isLooted = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (PlayerPrefs.HasKey("LootBox") == false)
        {
            isLooted = 0;
            PlayerPrefs.SetInt("LootBox", isLooted);
        }
        else
            isLooted = PlayerPrefs.GetInt("LootBox");

        if(isLooted == 1)
        {
            GetComponent<SpriteRenderer>().sprite = opened;
            animator.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isLooted == 0)
            {
                OpenLootBox();
            }
        }
    }

    public void OpenLootBox()
    {
        animator.SetTrigger("Open");
        isLooted = 1;
        PlayerPrefs.SetInt("LootBox", isLooted);

    }

}
