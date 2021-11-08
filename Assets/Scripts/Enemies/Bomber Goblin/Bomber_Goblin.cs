using UnityEngine;

public class Bomber_Goblin : MonoBehaviour, IDamageable
{
    #region Animation Variables
    private const string Attack = "Attack";
    private const string Hit = "Hit";
    private const string Death = "Death";
    #endregion

    public float speed = 20f;
    public float gravity = 9.81f;
    public bool lowAngle = true;
    public float shotDelay = 0.5f;

    [SerializeField] private EnemyData enemyData;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform throwPosition;

    private Vector2 direction;
    private AIDetector aIDetector;
    private Animator animator;
    private EnemyHealth enemyHealth;
    private float currentHealth;

    private int facingDirection;

    public GameObject target;

    private bool canThrow = true;

    private Vector2 throwDirection;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        aIDetector = GetComponent<AIDetector>();
        currentHealth = enemyData.maxHealth;
        facingDirection = 1;
        direction = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {

        if (aIDetector.PlayerDetected)
        {
            target = aIDetector.Target;
            direction = target.transform.position - transform.position;
            throwDirection = CalculateDirection();

            if (throwDirection != Vector2.zero && Vector2.Distance(target.transform.position, transform.position) > 3)
            {
                animator.SetTrigger(Attack);
                //ThrowBomb();
            }
        }


        if (direction.x <= 0 && facingDirection == 1 || direction.x >= 0 && facingDirection == -1)
            Flip();
    }

    public void TakeDame(int amountOfDame)
    {
        // currentHealth -= amountOfDame;
        if (currentHealth != 0)
            animator.SetTrigger(Hit);

        if (currentHealth <= 0)
        {
            animator.SetBool(Death, true);
            GetComponent<BoxCollider2D>().enabled = false;
        }

    }

    void ThrowBomb()
    {
        if (canThrow)
        {
            GameObject bomb = Instantiate(bombPrefab, throwPosition.transform.position, bombPrefab.transform.rotation);
            bomb.GetComponent<Rigidbody2D>().velocity = throwDirection * speed;
            Debug.Log(throwDirection);

            canThrow = false;
            Invoke("CanThrowAgain", shotDelay);
        }

    }
    void CanThrowAgain() => canThrow = true;

    float? CalculateAngle(bool low)
    {
        Vector2 targetDir = (Vector2)target.transform.position - (Vector2)throwPosition.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;

        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            else
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
        }
        else
            return null;
    }

    Vector2 CalculateDirection()
    {
        float? angle = CalculateAngle(lowAngle);

        Vector2 direction = Vector2.zero;
        if (angle != null)
        {
            float y = Mathf.Tan((float)angle);
            direction = new Vector2(1f, y);
            return (direction * facingDirection).normalized;
        }

        return Vector2.zero;
    }

    private void Flip()
    {
        facingDirection *= -1;
        Vector3 _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
    }

    private void Destroy() => Destroy(this.gameObject);
}
