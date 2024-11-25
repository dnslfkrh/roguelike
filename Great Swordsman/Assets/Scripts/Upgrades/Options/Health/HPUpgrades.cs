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
}