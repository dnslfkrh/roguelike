using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    [SerializeField]
    private string bossName;
   
    private Rigidbody2D target;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private BossStatsManager statsManager;
    private Slider bossHPSlider;

    private float moveSpeed;
    private float hp;
    private float maxHP;
    private float attackPower;
    private float attackSpeed;
    private float lastAttackTime;

    private float minChaseDistance;
    private float maxChaseDistance;
    private float knockbackForce;
    private bool isLive = true;
    private bool isKnockedBack = false;

    private void Awake()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        statsManager = FindObjectOfType<BossStatsManager>();
        bossHPSlider = GetComponentInChildren<Slider>();
        knockbackForce = GameManager.Instance.weaponManager.knockbackForce;

        InitializeBossStats();
    }

    private void InitializeBossStats()
    {
        var stats = statsManager.GetBossStats(bossName);
       
        moveSpeed = stats.MoveSpeed;
        maxHP = stats.HP;
        hp = maxHP;
        attackPower = stats.AttackPower;
        attackSpeed = stats.AttackSpeed;
       
        minChaseDistance = stats.MinChaseDistance;
        maxChaseDistance = stats.MaxChaseDistance;

        UpdateHPSlider();
    }

    private void Update()
    {
        if (!isLive || isKnockedBack)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(rigid.position, target.position);

        if (distanceToPlayer <= minChaseDistance || distanceToPlayer >= maxChaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            StayIdle();
        }
    }

    private void StayIdle()
    {
        rigid.velocity = Vector2.zero;
    }

    private void ChasePlayer()
    {
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;

        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void UpdateHPSlider()
    {
        if (bossHPSlider != null)
        {
            bossHPSlider.maxValue = maxHP;
            bossHPSlider.value = hp;
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

        UpdateHPSlider();

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

    private void Die()
    {
        isLive = false;
        GameManager.Instance.GetExp();
        gameObject.SetActive(false);
        GameManager.Instance.playerHP.Vampiric();
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
}