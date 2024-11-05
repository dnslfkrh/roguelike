using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string enemyName; // 인스펙터에서 설정할 캐릭터명 (Brook, Jinbe, Usopp, Franky)

    public Rigidbody2D target;
    private bool isLive = true;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;

    private float moveSpeed;
    private float hp;
    private float attackPower;
    private float attackSpeed;
    private float lastAttackTime;

    private EnemyStatsManager statsManager;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        statsManager = FindObjectOfType<EnemyStatsManager>();
        if (statsManager == null)
        {
            Debug.LogError("Scene에 EnemyStatsManager가 없습니다!");
            return;
        }

        InitializeStats();
    }


    private void InitializeStats()
    {
        moveSpeed = statsManager.GetMoveSpeed(enemyName);
        hp = statsManager.GetHP(enemyName);
        attackPower = statsManager.GetAttackPower(enemyName);
        attackSpeed = statsManager.GetAttackSpeed(enemyName);
    }

    private void FixedUpdate()
    {
        if (!isLive)
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isLive) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastAttackTime + attackSpeed)
            {
                PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();
                if (playerHP != null)
                {
                    playerHP.TakeDamage(attackPower);
                    lastAttackTime = Time.time;

                    Debug.Log($"{enemyName}가 플레이어에게 {attackPower} 데미지를 입혔습니다.");
                }
            }
        }
    }

    public void Die()
    {
        isLive = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void TakeDamage(float damage)
    {
        if (!isLive)
            return;

        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    public float GetHP() => hp;
    public float GetAttackPower() => attackPower;
    public float GetMoveSpeed() => moveSpeed;
}