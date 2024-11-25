namespace Upgrades.Combat
{
    using Upgrades.Core;

    public class PlayerDamageUpgrade : IUpgrade
    {
        public string Name => "Player Damage +5";
        public string Description => "���ݷ��� 5 ������ŵ�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("+", 5);
        }
    }

    public class DoubleDamageHalfSpeedUpgrade : IUpgrade
    {
        public string Name => "Double Damage, Half Sword Speed";
        public string Description => "���ݷ� 2��, ���� ȸ���ӵ� ����";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("*", 2);
            components.WeaponManager.ChangeRotationSpeed("/", 2);
        }
    }

    public class DoubleDamageHalfMaxHP : IUpgrade
    {
        public string Name => "Double Damage, Half Max HP";
        public string Description => "���ݷ� 2��, �ִ� ü�� ����";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("*", 2);
            components.PlayerHP.ChangeMaxHPHalf();
        }
    }
}