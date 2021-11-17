using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public float rotationSpeed = 120;
    public float speed = 2;
    public Vector2 dir;

    private Animator animator;
    private Rigidbody2D rigid;
    private bool isExposion = false;
    private float timeDestroy = 3f;

    // Start is called before the first frame update
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        if (isExposion == false)
        {
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * dir;

            Quaternion toRoration = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRoration, rotationSpeed * Time.deltaTime);
        }
        else
            dir = Vector2.zero;
    }

    private void FixedUpdate()
    {
        rigid.velocity = speed * Time.deltaTime * dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetTrigger("Explosion");
        isExposion = true;
    }

    private void Destroy() => Destroy(gameObject);
}
