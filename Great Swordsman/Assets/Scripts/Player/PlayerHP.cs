using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerHP : MonoBehaviour
{
    public float currentHP;
    public float maxHP;
    public UnityEvent onPlayerDeath;
    public UnityEvent<float> onHealthChanged;
    private bool isDead = false;
    private bool isRegenerating = false;
    private bool canSurviveOnce = false;
    public float refenerateValue = 30;
    public float vampiricValue = 10;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void SetMaxHealth(float maxHealth)
    {
        maxHP = maxHealth;
        currentHP = maxHP;
    }

    public void ChangeHP(string type, int value)
    {
        if (type == "currentHP")
        {
            currentHP += value;
        }
        else if (type == "maxHP")
        {
            maxHP += value;
        }

        if (currentHP <= 0)
        {
            currentHP = 100;
        }

        if (maxHP <= 0)
        {
            maxHP = 100;
        }

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        UpdateHealthBar();
    }

    public void ChangeMaxHPHalf()
    {
        maxHP /= 2;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
            Debug.Log("현재 체력을 최대 체력에: " + currentHP);
        }

        Debug.Log("최대 체력 반토막: " + maxHP);
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHP = Mathf.Max(0f, currentHP - damage);
        Debug.Log("공격 받아서 현재 체력: " + currentHP);

        UpdateHealthBar();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        float healthRatio = (float)currentHP / maxHP;
        Debug.Log($"currentHP: {currentHP}, maxHP: {maxHP}, healthRatio: {healthRatio}");
        onHealthChanged?.Invoke(healthRatio);
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        if (canSurviveOnce)
        {
            currentHP = 1;
            canSurviveOnce = false;
            Debug.Log("견뎌");
            UpdateHealthBar();
            return;
        }

        isDead = true;
        Debug.Log("Player died!");

        GameManager.Instance.GameDefeat();
    }

    public void CanSurviveOnce()
    {
        canSurviveOnce = true;
    }

    public void StartHPRegeneration()
    {
        if (!isRegenerating)
        {
            isRegenerating = true;
            StartCoroutine(RegenerateHealth());
        }
    }

    public void ChangeRefenerateValue(float value)
    {
        refenerateValue += value;
        Debug.Log("체력 재생력 증가: " + refenerateValue);
    }

    private IEnumerator RegenerateHealth()
    {
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
    public void Vampiric()
    {
        if (GameManager.Instance.player.isVampiric == true)
        {
            currentHP += (GameManager.Instance.player.attackDamage / vampiricValue);
            Debug.Log("피해 흡혈");
            UpdateHealthBar();
        }
    }

    public void ChangeVampiricValue()
    {
        vampiricValue -= 5;
        Debug.Log("피해 흡혈 감소" + vampiricValue);
    }

    public float GetCurrentHealth() => currentHP;
    public float GetMaxHealth() => maxHP;
    public bool IsDead() => isDead;
}