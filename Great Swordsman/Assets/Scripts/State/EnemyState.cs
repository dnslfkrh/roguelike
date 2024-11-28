public interface IEnemyState
{
    void ApplyEffect(Enemy enemy);
    void ResetEffect(Enemy enemy);
}

public class IceEffect : IEnemyState
{
    private float slowDuration;
    private float slowAmount;
    private float originalMoveSpeed;

    public IceEffect(float duration, float amount)
    {
        slowDuration = duration;
        slowAmount = amount;
    }

    public void ApplyEffect(Enemy enemy)
    {
        originalMoveSpeed = enemy.GetMoveSpeed();
        enemy.ApplySlow(slowDuration, slowAmount);
    }

    public void ResetEffect(Enemy enemy)
    {
        enemy.InitializeStats();
    }
}

public class FireEffect : IEnemyState
{
    private float burnDuration;
    private float burnDamage;

    public FireEffect(float duration, float damage)
    {
        burnDuration = duration;
        burnDamage = damage;
    }

    public void ApplyEffect(Enemy enemy)
    {
        enemy.ApplyBurn(burnDuration, burnDamage);
    }

    public void ResetEffect(Enemy enemy)
    {
        // ¹¹ ÇÒ°Ô ÀÖ³ª..
    }
}
