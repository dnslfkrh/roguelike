namespace Upgrades.Skill
{
    using UnityEngine;
    using Upgrades.Core;

    public class UnlockDashSkillUpgrade : IUpgrade
    {
        public string Name => "Unlock Dash Skill";
        public string Description => "스페이스바로 사용할 수 있는 대쉬 스킬을 잠금 해제합니다";

        public void Apply(PlayerComponents components)
        {
            components.SkillManager.UnlockDashSkill();
        }
    }

    public class DashSkillDistanceUpgrade : IUpgrade
    {
        public string Name => "Dash Skill Distance Upgrade";
        public string Description => "스페이스바로 사용할 수 있는 대쉬 스킬의 이동 거리가 증가됩니다";

        public void Apply(PlayerComponents components)
        {
            components.SkillManager.IncreaseDashDistance();
        }
    }

    public class DashSkillCooldownUpgrade : IUpgrade
    {
        public string Name => "Dash Skill Distance Upgrade";
        public string Description => "스페이스바로 사용할 수 있는 대쉬 스킬의 쿨타임이 감소됩니다";

        public void Apply(PlayerComponents components)
        {
            components.SkillManager.DecreaseDashCooldown();
        }
    }
}