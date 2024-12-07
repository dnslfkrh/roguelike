namespace Upgrades.Combat
{
    using Upgrades.Core;

    public class PlayerDamageUpgrade : IUpgrade
    {
        public string Name => "Player Damage +25";
        public string Description => "���ݷ��� 25 ������ŵ�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("+", 25);
        }
    }

    public class DoubleDamageHalfSpeedUpgrade : IUpgrade
    {
        public string Name => "Double Damage, Half Sword Speed";
        public string Description => "���ݷ��� 2�谡 ������, ���� ȸ�� �ӵ��� �������� �����մϴ�";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("*", 2);
            components.WeaponManager.ChangeRotationSpeed("/", 2);
        }
    }

    public class DoubleDamageHalfMaxHPUpgrade : IUpgrade
    {
        public string Name => "Double Damage, Half Max HP";
        public string Description => "���ݷ��� 2�谡 ������, �ִ� ü���� �������� �����մϴ�";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("*", 2);
            components.PlayerHP.ChangeMaxHPHalf();
        }
    }

    public class IncreaseKnockbackForceUpgrade : IUpgrade
    {
        public string Name => "Increase Enemy Knockback";
        public string Description => "���� �� �ָ� �о���ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.IncreaseKnockbackForce();
        }
    }

    public class FreeExpUpgrade : IUpgrade
    {
        public string Name => "Free Exp";
        public string Description => "����ġ�� �帳�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.Player.GetExpFromOption();
        }
    }

    public class SwordRotateDirectionUpgrade : IUpgrade
    {
        public string Name => "Change Sword Rotate Direction";
        public string Description => "Į ȸ�� ������ �ٲߴϴ�";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.ChangeRotationDirection();
        }
    }

    public class ExtraExpUpgrade : IUpgrade
    {
        public string Name => "Get Extra Exp";
        public string Description => "�߰� ����ġ�� ���� �� �ֽ��ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeExtraExp();
        }
    }
}