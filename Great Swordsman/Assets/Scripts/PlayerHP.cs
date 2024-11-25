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

    public float GetCurrentHealth() => currentHP;
    public float GetMaxHealth() => maxHP;
    public bool IsDead() => isDead;
}