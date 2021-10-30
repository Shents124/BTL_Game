using UnityEngine;

public class Stone : MonoBehaviour
{
    private Rigidbody2D rigid;
    private float massPush = 1.5f;
    private float massStatic = 200f;

    private void OnEnable()
    {
        EventBroker.OnPushing += OnMove;
        EventBroker.OnStopPushing += OnStop;
    }
    private void OnDisable()
    {
        EventBroker.OnPushing -= OnMove;
        EventBroker.OnStopPushing -= OnStop;
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        OnStop();
    }

    public void OnMove() => rigid.mass = massPush;

    public void OnStop() => rigid.mass = massStatic;

}
