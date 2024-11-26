namespace Upgrades.Skill
{
    using UnityEngine;
    using Upgrades.Core;

    public class UnlockDashSkillUpgrade : IUpgrade
    {
        public string Name => "Unlock Dash Skill";
        public string Description => "�����̽��ٷ� ����� �� �ִ� �뽬 ��ų�� ��� �����մϴ�";

        public void Apply(PlayerComponents components)
        {
            components.SkillManager.UnlockDashSkill();
        }
    }

    public class DashSkillDistanceUpgrade : IUpgrade
    {
        public string Name => "Dash Skill Distance Upgrade";
        public string Description => "�����̽��ٷ� ����� �� �ִ� �뽬 ��ų�� �̵� �Ÿ��� �����˴ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.SkillManager.IncreaseDashDistance();
        }
    }

    public class DashSkillCooldownUpgrade : IUpgrade
    {
        public string Name => "Dash Skill Distance Upgrade";
        public string Description => "�����̽��ٷ� ����� �� �ִ� �뽬 ��ų�� ��Ÿ���� ���ҵ˴ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.SkillManager.DecreaseDashCooldown();
        }
    }
}