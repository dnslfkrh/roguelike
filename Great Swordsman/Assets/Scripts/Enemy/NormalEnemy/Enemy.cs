using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string enemyName;

    private Rigidbody2D target;
    private bool isLive = true;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;

    private float moveSpeed;
    private float hp;
    private float attackPower;
    private float attackSpeed;
    private float lastAttackTime;
    private float knockbackForce;
    private bool isKnockedBack = false;
    private IEnemyState currentState;
    private Coroutine burnCoroutine;
    private EnemyStatsManager statsManager;

    private void Awake()
    {
        target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        statsManager = FindObjectOfType<EnemyStatsManager>();

        InitializeStats();

        knockbackForce = GameManager.Instance.WeaponManager.knockbackForce;
    }

    public void InitializeStats()
    {
        moveSpeed = statsManager.GetMoveSpeed(enemyName);
        hp = statsManager.GetHP(enemyName);
        attackPower = statsManager.GetAttackPower(enemyName);
        attackSpeed = statsManager.GetAttackSpeed(enemyName);
    }

    private void Update()
    {
        if (!isLive || isKnockedBack)
        {
            return;
        }

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }
        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        isLive = true;
        isKnockedBack = false;
        GetComponent<CapsuleCollider2D>().enabled = true;

        InitializeStats();

        if (rigid != null)
        {
            rigid.velocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isLive)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastAttackTime + attackSpeed)
            {
                PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();
                if (playerHP != null)
                {
                    playerHP.TakeDamage(attackPower);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isLive)
        {
            return;
        }

        hp -= damage;
        StartCoroutine(Knockback());

        if (hp <= 0)
        {
            Die();
        }
    }

    private IEnumerator Knockback()
    {
        isKnockedBack = true;
        rigid.velocity = Vector2.zero;

        Vector2 playerPosition = GameManager.Instance.Player.transform.position;
        Vector2 dirVec = (Vector2)transform.position - playerPosition;

        float adjustedKnockbackForce = (currentState is FireEffect) ? knockbackForce * 0.33f : knockbackForce;

        rigid.AddForce(dirVec.normalized * adjustedKnockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        rigid.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    public void Die()
    {
        GameManager.Instance.GetExp(1);
        isLive = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GameManager.Instance.PlayerHP.Vampiric();
        gameObject.SetActive(false);
    }

    public void SetState(IEnemyState newState)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        currentState = newState;

        if (newState is IceEffect)
        {
            spriter.color = Color.cyan;
        }
        else if (newState is FireEffect)
        {
            spriter.color = Color.red;
        }

        currentState?.ApplyEffect(this);
        StartCoroutine(ResetStateAfterDuration(5f));
    }

    private IEnumerator ResetStateAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        spriter.color = Color.white;
        currentState?.ResetEffect(this);
        currentState = null;
    }

    public void ApplySlow(float duration, float amount)
    {
        moveSpeed *= (1 - amount);
        StartCoroutine(ResetSpeedAfterDuration(duration));
    }

    private IEnumerator ResetSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        InitializeStats();
    }

    public void ApplyBurn(float duration, float damagePerSecond)
    {
        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }

        burnCoroutine = StartCoroutine(BurnEffect(duration, damagePerSecond));
    }

    private IEnumerator BurnEffect(float duration, float damagePerSecond)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            TakeDamage(damagePerSecond);
            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }

        burnCoroutine = null;
    }

    public float GetHP() => hp;
    public float GetAttackPower() => attackPower;
    public float GetMoveSpeed() => moveSpeed;
}