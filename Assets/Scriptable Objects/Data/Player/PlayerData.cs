using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 2;
    public int defaultGravityScale = 3;
    public GameObject jumpEffect;
    public GameObject landEffect;

    [Header("Attack State")]
    public float knockBackVelocity = 10f;
    public int dame = 1;
    public Vector2 attackSize;
    public float angle;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public GameObject hitEffect;
    

    [Header("Dash State")]
    public int amountOfDash = 1;
    public float dashCoolDown = 0.5f;
    public int dashVelocity = 10;
    public float distanceBetweenImages = 0.1f;

    [Header("Push State")]
    public LayerMask whatIsPush;
    public float pushVelocity = 5f;

    [Header("Check Veriables")]
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    public LayerMask WhatIsWall;
}
