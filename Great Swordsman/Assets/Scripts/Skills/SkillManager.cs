using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player player;
    public int dashDistance = 2; // �⺻ �뽬 �Ÿ�
    public int dashCooldown = 5; // �⺻ �뽬 ��Ÿ��
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
            Debug.Log("�뽬 ��ų ���");
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
        Debug.Log("�뽬 ��ų ��� ����");
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
