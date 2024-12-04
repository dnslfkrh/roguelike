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
            Debug.Log("���� ü���� �ִ� ü�¿�: " + currentHP);
        }

        Debug.Log("�ִ� ü�� ���丷: " + maxHP);
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHP = Mathf.Max(0f, currentHP - damage);
        Debug.Log("���� �޾Ƽ� ���� ü��: " + currentHP);

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
            Debug.Log("�ߵ�");
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
        Debug.Log("ü�� ����� ����: " + refenerateValue);
    }

    private IEnumerator RegenerateHealth()
    {
        while (isRegenerating)
        {
            if (currentHP >= maxHP || currentHP + refenerateValue >= maxHP)
            {
                currentHP = maxHP;
                Debug.Log("ü���� ������ ȸ�� ����" + currentHP);
            }
            else
            {
                currentHP += refenerateValue;
                Debug.Log("ü���� ���� ȸ��" + currentHP);
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
            Debug.Log("���� ����");
            UpdateHealthBar();
        }
    }

    public void ChangeVampiricValue()
    {
        vampiricValue -= 5;
        Debug.Log("���� ���� ����" + vampiricValue);
    }

    public float GetCurrentHealth() => currentHP;
    public float GetMaxHealth() => maxHP;
    public bool IsDead() => isDead;
}