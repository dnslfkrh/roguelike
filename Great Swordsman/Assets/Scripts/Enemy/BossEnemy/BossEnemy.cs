using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{
    [SerializeField]
    private string bossName;

    public Rigidbody2D target;
    public Rigidbody2D rigid;
    public BossStatsManager statsManager;
    public Slider bossHPSlider;
    private SpriteRenderer spriter;

    private float moveSpeed;
    private float hp;
    private float maxHP;
    private float attackPower;
    private float attackSpeed;
    private float lastAttackTime;

    private float minChaseDistance;
    private float maxChaseDistance;
    private float knockbackForce = 1;
    private bool isLive = true;
    private bool isKnockedBack = false;
    private bool isIdleMoving = false;

    public void Awake()
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        statsManager = FindObjectOfType<BossStatsManager>();

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            if (bossHPSlider != null)
            {
                bossHPSlider = Instantiate(bossHPSlider, canvas.transform);

                BossHP bossHPComponent = bossHPSlider.GetComponent<BossHP>();
                if (bossHPComponent != null)
                {
                    bossHPComponent.target = this.transform;
                }
            }
        }
    }

    public void Start()
    {
        InitializeBossStats();
    }

    private void InitializeBossStats()
    {
        var stats = statsManager.GetBossStats(bossName);
        if (stats == null)
        {
            return;
        }

        moveSpeed = stats.MoveSpeed;
        maxHP = stats.HP;
        hp = maxHP;
        attackPower = stats.AttackPower;
        attackSpeed = stats.AttackSpeed;

        minChaseDistance = stats.MinChaseDistance;
        maxChaseDistance = stats.MaxChaseDistance;

        Debug.Log(minChaseDistance + " 이하일 때 따라가고, " + maxChaseDistance + " 이상일 때 따라감");

        UpdateHPSlider();
    }

    public void Update()
    {
        if (!isLive || isKnockedBack)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(rigid.position, target.position);

        if (distanceToPlayer <= minChaseDistance || distanceToPlayer > maxChaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            StayIdle();
        }
    }

    private void UpdateHPSlider()
    {
        if (bossHPSlider != null)
        {
            bossHPSlider.maxValue = maxHP;
            bossHPSlider.value = hp;
        }
    }

    private void StayIdle()
    {
        if (!isIdleMoving)
        {
            StartCoroutine(RandomIdleMovement());
        }
    }

    private IEnumerator RandomIdleMovement()
    {
        isIdleMoving = true;

        Vector2 randomDirection1 = Random.insideUnitCircle.normalized * 2f;
        rigid.velocity = randomDirection1;
        yield return new WaitForSeconds(2f);

        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);

        Vector2 randomDirection2 = Random.insideUnitCircle.normalized * 2f;
        rigid.velocity = randomDirection2;
        yield return new WaitForSeconds(2f);

        rigid.velocity = Vector2.zero;

        isIdleMoving = false;
    }

    private void ChasePlayer()
    {
        Vector2 direction = (target.position - rigid.position).normalized;
        rigid.velocity = (direction * moveSpeed) / 10;

        spriter.flipX = target.position.x < rigid.position.x;
    }

    public void TakeDamage(float damage)
    {
        if (!isLive)
        {
            return;
        }

        hp -= damage;

        StartCoroutine(Knockback());

        GameManager.Instance.playerHP.Vampiric();

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
        GameManager.Instance.GetExp(10);
        Destroy(gameObject);
        Destroy(bossHPSlider.gameObject);
    }

    public void OnCollisionStay2D(Collision2D collision)
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
