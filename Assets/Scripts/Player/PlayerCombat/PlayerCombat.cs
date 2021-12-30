using UnityEngine;
using UnityEngine.Events;

public class PlayerCombat : MonoBehaviour, IDamageable, IKnockbackable
{
    public UnityEvent OnUpdateHealthUI;
    public bool isDeath;
    public bool isVictory;
    [Header("Health Variables")]
    [SerializeField] private FloatVariable currentHealth;
    [SerializeField] private FloatVariable maxHealth;

    [Header("Attack Variables")]
    [SerializeField] private Transform attackPointPos;
    [SerializeField] private PlayerData playerData;


    private Player player;
    private bool isDamaged;


    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void OnEnable()
    {
        EventBroker.OnBossDead += Victory;
    }
    private void OnDisable()
    {
        EventBroker.OnBossDead -= Victory;
    }
    private void Start()
    {
        if (currentHealth.value <= 0)
            currentHealth.value = maxHealth.value;

        isDamaged = false;
        isDeath = false;
        isVictory = false;
    }

    public void DoingDamage()
    {
        Collider2D[] hitEnemies =
        Physics2D.OverlapCapsuleAll(attackPointPos.position, playerData.attackSize, CapsuleDirection2D.Horizontal, playerData.angle, playerData.whatIsEnemy);

        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                //Instantiate(playerData.hitEffect, attackPointPos.transform.position, Quaternion.identity);
                enemy.GetComponent<IDamageable>().TakeDame(playerData.dame, attackPointPos.transform.position);
                //enemy.GetComponent<IKnockbackable>().KnockBack(player.FacingDirection * playerData.knockBackVelocity);
            }
        }
    }

    public void TakeDame(int amountOfDame, Vector3 damePos)
    {
        if (currentHealth.value >= 0 && isDeath == false && isVictory == false)
        {
            currentHealth.value -= amountOfDame;
            OnUpdateHealthUI?.Invoke();
            isDamaged = true;
            player.loseHPAuidoPlayer.PlayRandomSound();
        }

        if (currentHealth.value <= 0 && isDeath == false)
        {
            currentHealth.value = 0;
            isDeath = true;
            StartCoroutine(player.DieRespawnCoroutine());
            player.audioManager.StopAllAudio();
            player.gameoverAuidoPlayer.PlayRandomSound();
        }
    }

    public bool GetIsDamaged() => isDamaged;
    public void SetIsDamaged(bool value) => isDamaged = value;

    public void KnockBack(float velocity)
    {
        player.SetVelocityX(velocity * -player.FacingDirection);
    }

    public void ResetHeath() => currentHealth.value = maxHealth.value;

    public int GetCurrentHealth() => currentHealth.value;

    private void Victory() => isVictory = true;
}
