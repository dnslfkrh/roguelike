namespace Upgrades.Health
{
    using Upgrades.Core;

    public class CurrentHPUpgrade : IUpgrade
    {
        public string Name => "Current HP +100";
        public string Description => "���� ü���� 100 ������ŵ�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.IncreaseHP("currentHP", 100);
        }
    }

    public class MaxHPUpgrade : IUpgrade
    {
        public string Name => "Max HP +200";
        public string Description => "�ִ� ü���� 200 ������ŵ�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.IncreaseHP("maxHP", 200);
        }
    }

    public class HPRegeneration : IUpgrade
    {
        public string Name => "HP Regeneration";
        public string Description => "ü���� ����˴ϴ�";
        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.StartHPRegeneration();
        }
    }
}