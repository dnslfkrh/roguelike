using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 1000f;
    private float currentHP;

    public UnityEvent onPlayerDeath;
    public UnityEvent<float> onHealthChanged;

    private bool isDead = false;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHP = Mathf.Max(0f, currentHP - damage);
        Debug.Log($"Player took {damage} damage. Current health: {currentHP}");

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

    public float GetCurrentHealth() => currentHP;
    public float GetMaxHealth() => maxHP;
    public bool IsDead() => isDead;
}