using System;
using UnityEngine;

public class Stone : MonoBehaviour, IDamageable
{
    public GameObject hitEffect;
    private Rigidbody2D rigid;
    private float massPush = 1.5f;
    private float massStatic = 200f;
    public bool isRespawn = false;
    public Vector2 startPos;

    private void OnEnable()
    {
        EventBroker.OnPushing += OnMove;
        EventBroker.OnStopPushing += OnStop;
        EventBroker.OnPlayerDead += Respawn;
    }
    private void OnDisable()
    {
        EventBroker.OnPushing -= OnMove;
        EventBroker.OnStopPushing -= OnStop;
        EventBroker.OnPlayerDead -= Respawn;
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        OnStop();
        startPos = (Vector2)transform.position + new Vector2(0, 1.5f);
    }

    public void OnMove() => rigid.mass = massPush;

    public void OnStop() => rigid.mass = massStatic;

    public void TakeDame(int amountOfDame, Vector3 damePos)
    {
        Instantiate(hitEffect, damePos, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spikes"))
            Respawn();
    }

    private void Respawn()
    {
        if(isRespawn)
            transform.position = startPos;
    }
}
