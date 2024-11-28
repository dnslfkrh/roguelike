using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    public float AttackPower { get; private set; }
    public float HP { get; private set; }
    public float MoveSpeed { get; private set; }
    public float AttackSpeed { get; private set; }

    public EnemyStats(float attackPower, float hp, float moveSpeed, float attackSpeed)
    {
        AttackPower = attackPower;
        HP = hp;
        MoveSpeed = moveSpeed;
        AttackSpeed = attackSpeed;
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
        enemyStatsDictionary.Add("Brook", new EnemyStats(70f, 20f, 2f, 1.2f));
        enemyStatsDictionary.Add("Jinbe", new EnemyStats(90f, 40f, 1f, 0.8f));
        enemyStatsDictionary.Add("Usopp", new EnemyStats(65f, 30f, 1.1f, 1.5f));
        enemyStatsDictionary.Add("Franky", new EnemyStats(85f, 25f, 0.7f, 0.7f));
    }

    public EnemyStats GetEnemyStats(string enemyName)
    {
        if (enemyStatsDictionary.TryGetValue(enemyName, out EnemyStats stats))
        {
            return stats;
        }
        else
        {
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

    public float GetAttackSpeed(string enemyName)
    {
        EnemyStats stats = GetEnemyStats(enemyName);
        return stats != null ? stats.AttackSpeed : 0f;
    }
}