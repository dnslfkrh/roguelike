using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    public float AttackPower { get; private set; }
    public float HP { get; private set; }
    public float MoveSpeed { get; private set; }

    public EnemyStats(float attackPower, float hp, float moveSpeed)
    {
        AttackPower = attackPower;
        HP = hp;
        MoveSpeed = moveSpeed;
    }
}

public class EnemyStatsManager : MonoBehaviour
{
    private Dictionary<string, EnemyStats> enemyStatsDictionary = new Dictionary<string, EnemyStats>();

    private void Awake()
    {
        InitializeEnemyStats();
    }

    private void InitializeEnemyStats()
    {
        // Brook: À½¾Ç°¡ ÇØ°ñ°Ë»ç
        enemyStatsDictionary.Add("Brook", new EnemyStats(70f, 60f, 2f));

        // Jinbe: Àü ¿ÕÇÏ Ä¥¹«ÇØ
        enemyStatsDictionary.Add("Jinbe", new EnemyStats(90f, 100f, 1f));

        // Usopp: Àú°Ý¿Õ °«¿ì¼Ù
        enemyStatsDictionary.Add("Usopp", new EnemyStats(65f, 50f, 1.1f));

        // Franky: »çÀÌº¸±× Á¶¼±°ø
        enemyStatsDictionary.Add("Franky", new EnemyStats(85f, 90f, 0.7f));
    }

    public EnemyStats GetEnemyStats(string enemyName)
    {
        if (enemyStatsDictionary.TryGetValue(enemyName, out EnemyStats stats))
        {
            return stats;
        }
        else
        {
            Debug.LogError($"Enemy not found: {enemyName}");
            return null;
        }
    }

    public float GetAttackPower(string enemyName)
    {
        EnemyStats stats = GetEnemyStats(enemyName);
        return stats != null ? stats.AttackPower : 0f;
    }

    public float GetHP(string enemyName)
    {
        EnemyStats stats = GetEnemyStats(enemyName);
        return stats != null ? stats.HP : 0f;
    }

    public float GetMoveSpeed(string enemyName)
    {
        EnemyStats stats = GetEnemyStats(enemyName);
        return stats != null ? stats.MoveSpeed : 0f;
    }
}