using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public float AttackPower;
    public float MaxHP;
    public float MoveSpeed;

    public PlayerStats(float attackPower, float maxHP, float moveSpeed)
    {
        AttackPower = attackPower;
        MaxHP = maxHP;
        MoveSpeed = moveSpeed;
    }
}
