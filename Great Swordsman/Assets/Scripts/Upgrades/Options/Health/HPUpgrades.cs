namespace Upgrades.Health
{
    using Upgrades.Core;

    public class CurrentHPUpgrade : IUpgrade
    {
        public string Name => "Current HP +100";
        public string Description => "현재 체력을 100 증가시킵니다";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeHP("currentHP", 100);
        }
    }

    public class MaxHPUpgrade : IUpgrade
    {
        public string Name => "Max HP +200";
        public string Description => "최대 체력을 200 증가시킵니다";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeHP("maxHP", 200);
        }
    }

    public class HPRegenerationUpgrade : IUpgrade
    {
        public string Name => "HP Regeneration";
        public string Description => "체력이 재생됩니다";
        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.StartHPRegeneration();
        }
    }

    public class IncreaseHPRegenerationUpgrade : IUpgrade
    {
        public string Name => "Increase HP Regeneration";
        public string Description => "*주의* 체력 재생력이 증가하지만 재생 효과가 없을 시 의미가 없습니다";
        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeRefenerateValue(20);
        }
    }

    public class VampiricUpdate : IUpgrade
    {
        public string Name => "Vampiric Update";
        public string Description => "피해 흡혈을 시작합니다";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeVampiric();
        }
    }

    public class VampiricValueUpdate : IUpgrade
    {
        public string Name => "Vampiric Value Update";
        public string Description => "피해 흡혈량이 증가합니다";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeVampiricValue();
        }
    }

    public class IncreaseMaxHPDecreaseDamageUpgrade : IUpgrade
    {
        public string Name => "Increase MaxHP Decrease Damage";
        public string Description => "최대 체력이 증가하고 공격력이 감소합니다";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeHP("maxHP", 100);
            components.Player.ChangeAttackDamage("+", -20);
        }
    }

    public class DecreaseMaxHPIncreaseDamageUpgrade : IUpgrade
    {
        public string Name => "Decrease MaxHP Increase Damage";
        public string Description => "최대 체력이 감소하고 공격력이 증가합니다";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeHP("maxHP", -100);
            components.Player.ChangeAttackDamage("+", 20);
        }
    }

    public class CanSurviveOnceUpgrade : IUpgrade
    {
        public string Name => "Can Survive nce";
        public string Description => "앞으로 단 1회 체력 1로 버틸 수 있습니다";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.CanSurviveOnce();
        }
    }
}