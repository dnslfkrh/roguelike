using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string enemyName; // 인스펙터에서 설정할 캐릭터명 (Brook, Jinbe, Usopp, Franky)

    private Rigidbody2D target;
    private bool isLive = true;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;

    private float moveSpeed;
    private float hp;
    private float attackPower;
    private float attackSpeed;
    private float lastAttackTime;
    private float knockbackForce = 5f;
    private bool isKnockedBack = false;

    private EnemyStatsManager statsManager;

    private void Awake()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        statsManager = FindObjectOfType<EnemyStatsManager>();

        InitializeStats();
    }

    private void InitializeStats()
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

        Vector2 playerPosition = GameManager.Instance.player.transform.position;
        Vector2 dirVec = (Vector2)transform.position - playerPosition;

        rigid.AddForce(dirVec.normalized * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f);

        rigid.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    public void Die()
    {
        isLive = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }

    public float GetHP() => hp;
    public float GetAttackPower() => attackPower;
    public float GetMoveSpeed() => moveSpeed;
}