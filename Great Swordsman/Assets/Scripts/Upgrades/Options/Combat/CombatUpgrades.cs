namespace Upgrades.Combat
{
    using Upgrades.Core;

    public class PlayerDamageUpgrade : IUpgrade
    {
        public string Name => "Player Damage +5";
        public string Description => "공격력을 5 증가시킵니다";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("+", 5);
        }
    }

    public class DoubleDamageHalfSpeedUpgrade : IUpgrade
    {
        public string Name => "Double Damage, Half Sword Speed";
        public string Description => "공격력 2배, 검의 회전속도 절반";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("*", 2);
            components.WeaponManager.ChangeRotationSpeed("/", 2);
        }
    }

    public class DoubleDamageHalfMaxHP : IUpgrade
    {
        public string Name => "Double Damage, Half Max HP";
        public string Description => "공격력 2배, 최대 체력 절반";

        public void Apply(PlayerComponents components)
        {
            components.Player.ChangeAttackDamage("*", 2);
            components.PlayerHP.ChangeMaxHPHalf();
        }
    }
}