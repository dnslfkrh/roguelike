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

    // 스탯 변수들
    private float moveSpeed;
    private float hp;
    private float attackPower;

    private EnemyStatsManager statsManager;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();

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
        if (string.IsNullOrEmpty(enemyName))
        {
            Debug.LogError("Enemy Name이 설정되지 않았습니다.");
            return;
        }

        moveSpeed = statsManager.GetMoveSpeed(enemyName);
        hp = statsManager.GetHP(enemyName);
        attackPower = statsManager.GetAttackPower(enemyName);

        Debug.Log($"{enemyName} 스탯 - 이동속도: {moveSpeed}, 체력: {hp}, 공격력: {attackPower}");
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