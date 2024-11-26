using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player player;
    public int dashDistance = 2; // 기본 대쉬 거리
    public int dashCooldown = 5; // 기본 대쉬 쿨타임
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
            Debug.Log("대쉬 스킬 사용");
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
        Debug.Log("대쉬 스킬 잠금 해제");
    }

    public void IncreaseDashDistance()
    {
        dashDistance += 2;
    }

    public void DecreaseDashCooldown()
    {
        dashCooldown -= 2;
    }
}
