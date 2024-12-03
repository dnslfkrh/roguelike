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

    public void Awake()
    {
        InitializeBossStats();
    }

    private void InitializeBossStats()
    {
        bossStatsDictionary.Add("Mihawk", new BossStats(1400f, 400f, 15f, 24f, 10f, 20f));
        bossStatsDictionary.Add("Shanks", new BossStats(1800f, 800f, 18f, 16f, 9f, 22f));
        bossStatsDictionary.Add("BlackBeard", new BossStats(1600f, 600f, 12f, 20f, 9f, 21f));
        bossStatsDictionary.Add("Aokiji", new BossStats(1500f, 500f, 35f, 14f, 9f, 19f));
        bossStatsDictionary.Add("Doflamingo", new BossStats(1700f, 700f, 16f, 18f, 23f, 7f));
        bossStatsDictionary.Add("BigMom", new BossStats(2000f, 1000f, 10f, 14f, 9f, 25f));
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

    public float GetMinChaseDistance(string bossName)
    {
        return GetBossStats(bossName)?.MinChaseDistance ?? 0f;
    }

    public float GetMaxChaseDistance(string bossName)
    {
        return GetBossStats(bossName)?.MaxChaseDistance ?? 0f;
    }
}