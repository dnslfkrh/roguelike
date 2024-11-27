namespace Upgrades.Health
{
    using Upgrades.Core;

    public class CurrentHPUpgrade : IUpgrade
    {
        public string Name => "Current HP +100";
        public string Description => "���� ü���� 100 ������ŵ�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeHP("currentHP", 100);
        }
    }

    public class MaxHPUpgrade : IUpgrade
    {
        public string Name => "Max HP +200";
        public string Description => "�ִ� ü���� 200 ������ŵ�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeHP("maxHP", 200);
        }
    }

    public class HPRegenerationUpgrade : IUpgrade
    {
        public string Name => "HP Regeneration";
        public string Description => "ü���� ����˴ϴ�";
        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.StartHPRegeneration();
        }
    }

    public class IncreaseHPRegenerationUpgrade : IUpgrade
    {
        public string Name => "Increase HP Regeneration";
        public string Description => "*����* ü�� ������� ���������� ��� ȿ���� ���� �� �ǹ̰� �����ϴ�";
        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeRefenerateValue(20);
        }
    }

    public class VampiricUpdate : IUpgrade
    {
        public string Name => "Vampiric Update";
        public string Description => "���� ������ �����մϴ�";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeVampiric();
        }
    }

    public class VampiricValueUpdate : IUpgrade
    {
        public string Name => "Vampiric Value Update";
        public string Description => "���� �������� �����մϴ�";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeVampiricValue();
        }
    }

    public class IncreaseMaxHPDecreaseDamageUpgrade : IUpgrade
    {
        public string Name => "Increase MaxHP Decrease Damage";
        public string Description => "�ִ� ü���� �����ϰ� ���ݷ��� �����մϴ�";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeHP("maxHP", 100);
            components.Player.ChangeAttackDamage("+", -20);
        }
    }

    public class DecreaseMaxHPIncreaseDamageUpgrade : IUpgrade
    {
        public string Name => "Decrease MaxHP Increase Damage";
        public string Description => "�ִ� ü���� �����ϰ� ���ݷ��� �����մϴ�";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.ChangeHP("maxHP", -100);
            components.Player.ChangeAttackDamage("+", 20);
        }
    }

    public class CanSurviveOnceUpgrade : IUpgrade
    {
        public string Name => "Can Survive nce";
        public string Description => "������ �� 1ȸ ü�� 1�� ��ƿ �� �ֽ��ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.PlayerHP.CanSurviveOnce();
        }
    }
}