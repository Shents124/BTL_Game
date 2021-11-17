using UnityEngine;

public class Boss_Combat : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDame(int amountOfDame)
    {
        currentHealth -= maxHealth;
        if (currentHealth <= 0)
            currentHealth = maxHealth;
    }
}
