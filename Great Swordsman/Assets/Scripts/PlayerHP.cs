using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 1000f;
    private float currentHP;

    public UnityEvent onPlayerDeath;
    public UnityEvent<float> onHealthChanged;

    private bool isDead = false;
    private bool isRegenerating = false;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void IncreaseHP(string type, int value)
    {
        if (type == "currentHP")
        {
            if (currentHP + value > maxHP)
            {
                currentHP = maxHP;
                return;
            }
            currentHP += value;
        }
        else if (type == "maxHP")
        {
            maxHP += value;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHP = Mathf.Max(0f, currentHP - damage);
        Debug.Log($"현재 체력: {currentHP}");

        UpdateHealthBar();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        float healthRatio = currentHP / maxHP;
        onHealthChanged?.Invoke(healthRatio);
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        Debug.Log("Player died!");
        onPlayerDeath?.Invoke();
    }

    public void StartHPRegeneration()
    {
        if (!isRegenerating)
        {
            isRegenerating = true;
            StartCoroutine(RegenerateHealth());
        }
    }

    private IEnumerator RegenerateHealth()
    {
        int refenerateValue = 30;

        while (isRegenerating)
        {
            if (currentHP >= maxHP || currentHP + refenerateValue >= maxHP)
            {
                currentHP = maxHP;
                Debug.Log("체력이 꽉차서 회복 안해" + currentHP);
            }
            else
            {
                currentHP += refenerateValue;
                Debug.Log("체력이 조금 회복" + currentHP);
            }

            UpdateHealthBar();
            yield return new WaitForSeconds(2f);
        }
    }

    public float GetCurrentHealth() => currentHP;
    public float GetMaxHealth() => maxHP;
    public bool IsDead() => isDead;
}