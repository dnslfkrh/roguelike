using System.Collections.Generic;
using UnityEngine;

public class BossStats
{
    public float AttackPower { get; private set; }
    public float HP { get; private set; }
    public float MoveSpeed { get; private set; }
    public float AttackSpeed { get; private set; }
    public float MinChaseDistance { get; private set; }
    public float MaxChaseDistance { get; private set; }

    public BossStats(float attackPower, float hp, float moveSpeed, float attackSpeed, float minChaseDistance, float maxChaseDistance)
    {
        AttackPower = attackPower;
        HP = hp;
        MoveSpeed = moveSpeed;
        AttackSpeed = attackSpeed;
        MinChaseDistance = minChaseDistance;
        MaxChaseDistance = maxChaseDistance;
    }
}

public class BossStatsManager : MonoBehaviour
{
    private Dictionary<string, BossStats> bossStatsDictionary = new Dictionary<string, BossStats>();

    private void Awake()
    {
        InitializeBossStats();
    }

    private void InitializeBossStats()
    {
        bossStatsDictionary.Add("Mihawk", new BossStats(1400f, 400f, 40f, 24f, 20f, 10f));
        bossStatsDictionary.Add("Shanks", new BossStats(1800f, 800f, 20f, 16f, 22f, 8f));
        bossStatsDictionary.Add("BlackBeard", new BossStats(1600f, 600f, 30f, 20f, 21f, 9f));
        bossStatsDictionary.Add("Aokiji", new BossStats(1500f, 500f, 35f, 22f, 19f, 11f));
        bossStatsDictionary.Add("Doflamingo", new BossStats(1700f, 700f, 25f, 18f, 23f, 7f));
        bossStatsDictionary.Add("BigMom", new BossStats(2000f, 1000f, 15f, 14f, 25f, 5f));
    }

    public BossStats GetBossStats(string bossName)
    {
        return bossStatsDictionary.TryGetValue(bossName, out BossStats stats) ? stats : null;
    }

    public float GetAttackPower(string bossName)
    {
        return GetBossStats(bossName)?.AttackPower ?? 0f;
    }

    public float GetHP(string bossName)
    {
        return GetBossStats(bossName)?.HP ?? 0f;
    }

    public float GetMoveSpeed(string bossName)
    {
        return GetBossStats(bossName)?.MoveSpeed ?? 0f;
    }

    public float GetAttackSpeed(string bossName)
    {
        return GetBossStats(bossName)?.AttackSpeed ?? 0f;
    }

    public (float min, float max) GetChaseDistances(string bossName)
    {
        var stats = GetBossStats(bossName);
        return stats != null ? (stats.MinChaseDistance, stats.MaxChaseDistance) : (0f, 0f);
    }
}