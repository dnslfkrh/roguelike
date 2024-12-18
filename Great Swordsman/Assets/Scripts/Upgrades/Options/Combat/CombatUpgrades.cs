namespace Upgrades.Combat
{
    using Upgrades.Core;

    public class PlayerDamageUpgrade : IUpgrade
    {
        public string Name => "Player Damage +25";
        public string Description => "공격력을 25 증가시킵니다";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("+", 25);
        }
    }

    public class DoubleDamageHalfSpeedUpgrade : IUpgrade
    {
        public string Name => "Double Damage, Half Sword Speed";
        public string Description => "공격력이 2배가 되지만, 검의 회전 속도가 절반으로 감소합니다";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("*", 2);
            components.WeaponManager.ChangeRotationSpeed("/", 2);
        }
    }

    public class DoubleDamageHalfMaxHPUpgrade : IUpgrade
    {
        public string Name => "Double Damage, Half Max HP";
        public string Description => "공격력이 2배가 되지만, 최대 체력이 절반으로 감소합니다";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("*", 2);
            components.PlayerHP.ChangeMaxHPHalf();
        }
    }

    public class IncreaseKnockbackForceUpgrade : IUpgrade
    {
        public string Name => "Increase Enemy Knockback";
        public string Description => "적을 더 멀리 밀어냅니다";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.IncreaseKnockbackForce();
        }
    }

    public class FreeExpUpgrade : IUpgrade
    {
        public string Name => "Free Exp";
        public string Description => "경험치를 드립니다";

        public void Apply(PlayerComponents components)
        {
            components.Player.GetExpFromOption();
        }
    }

    public class SwordRotateDirectionUpgrade : IUpgrade
    {
        public string Name => "Change Sword Rotate Direction";
        public string Description => "칼 회전 방향을 바꿉니다";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.ChangeRotationDirection();
        }
    }

    public class ExtraExpUpgrade : IUpgrade
    {
        public string Name => "Get Extra Exp";
        public string Description => "추가 경험치를 얻을 수 있습니다";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeExtraExp();
        }
    }
}