using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player player;
    public int dashDistance = 2;
    public int dashCooldown = 5;
    public bool isDashUnlocked = false;
    private float dashCooldownRemaining = 0f;

    private void Update()
    {
        if (isDashUnlocked)
        {
            HandleDashSkill();
        }
    }

    private void HandleDashSkill()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownRemaining <= 0f)
        {
            Dash();
            dashCooldownRemaining = dashCooldown;
        }
        else if (dashCooldownRemaining > 0f)
        {
            dashCooldownRemaining -= Time.deltaTime;
        }
    }

    private void Dash()
    {
        player.Dash(dashDistance);
        dashCooldownRemaining = dashCooldown;
    }

    public void UnlockDashSkill()
    {
        isDashUnlocked = true;
    }

    public void IncreaseDashDistance(int value)
    {
        dashDistance += value;
    }

    public void DecreaseDashCooldown(int value)
    {
        dashCooldown -= value;
    }
}
