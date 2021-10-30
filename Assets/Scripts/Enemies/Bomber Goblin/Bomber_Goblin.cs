using UnityEngine;

public class Bomber_Goblin : MonoBehaviour, IDamageable
{
    #region Animation Variables
    private const string Attack = "Attack";
    private const string Hit = "Hit";
    private const string Death = "Death";
    #endregion

    [SerializeField] private EnemyData enemyData;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform throwPosition;
    [SerializeField] private float t;
    [SerializeField] private float launchForce;
    [SerializeField] private float timeAttackDelay = 0.3f;
    private Vector2 direction;
    private AIDetector aIDetector;
    private Animator animator;
    private EnemyHealth enemyHealth;
    private float currentHealth;
    private float startTimeAttack = 0f;
    private bool isFacingRight;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        aIDetector = GetComponent<AIDetector>();
        currentHealth = enemyData.maxHealth;
        isFacingRight = true;
        direction = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        //CalculateDirectionThrowBomb(t);

        if (aIDetector.PlayerDetected && (Time.time - startTimeAttack) >= timeAttackDelay)
        {
            animator.SetTrigger(Attack);
            startTimeAttack = Time.time;
        }

        if(direction.x <= 0 && isFacingRight || direction.x >= 0 && isFacingRight == false)
            Flip();
    }
    private void FixedUpdate()
    {
        CalculateDirectionThrowBomb(Time.deltaTime);
    }

    public void TakeDame(int amountOfDame)
    {
        currentHealth -= amountOfDame;
        Debug.Log(currentHealth);
        if (currentHealth != 0)
            animator.SetTrigger(Hit);

        if (currentHealth <= 0)
        {
            animator.SetBool(Death, true);
            GetComponent<BoxCollider2D>().enabled = false;
        }

    }

    private void ThrowBomb()
    {
        GameObject bombObject = Instantiate(bombPrefab, throwPosition.position, Quaternion.identity);
        bombObject.GetComponent<Rigidbody2D>().velocity = direction.normalized * launchForce * Time.deltaTime;
    }

    private void CalculateDirectionThrowBomb(float t)
    {
        if (aIDetector.PlayerDetected)
            direction = ((Vector2)aIDetector.Target.transform.position - 0.5f * Physics2D.gravity * t * t) / (launchForce * t);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }

    private void Destroy() => Destroy(this.gameObject);
}
