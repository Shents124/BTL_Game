using UnityEngine;

public class TrapSuspended : MonoBehaviour
{
    public float speed;
    public float leftAngle;
    public float rightAngle;

    private Rigidbody2D rigid;
    private bool movingClosewise;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        movingClosewise = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void ChangeMoveDir()
    {
        if (transform.rotation.z > rightAngle)
            movingClosewise = true;
        if (transform.rotation.z < leftAngle)
            movingClosewise = false;
    }

    public void Move()
    {
        ChangeMoveDir();

        if (movingClosewise)
            rigid.angularVelocity = -speed * Time.deltaTime;
        else
            rigid.angularVelocity = speed * Time.deltaTime;
    }
}
