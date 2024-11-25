namespace Upgrades.Weapons
{
    using Upgrades.Core;

    public class AddSwordUpgrade : IUpgrade
    {
        public string Name => "Add Sword";
        public string Description => "새로운 검을 추가합니다";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.IncreaseWeaponCount(1);
        }
    }

    public class WeaponRotationSpeedUpgrade : IUpgrade
    {
        public string Name => "Increase Weapon Rotation Speed";
        public string Description => "무기 회전 속도를 증가시킵니다";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.ChangeRotationSpeed("+", 50);
        }
    }

    public class WeaponDistanceUpgrade : IUpgrade
    {
        private readonly float distanceChange;
        private readonly string direction;

        public WeaponDistanceUpgrade(float change, bool increase)
        {
            distanceChange = change;
            direction = increase ? "Increase" : "Decrease";
        }

        public string Name => $"{direction} Weapon Distance From Player";
        public string Description => $"무기의 회전 반경을 {(direction == "Increase" ? "증가" : "감소")}시킵니다";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.ChangeDistanceFromPlayer(distanceChange);
        }
    }

    public class AddSwordsAndDecreaseDamageUpgrade: IUpgrade
    {
        public string Name => "Add Swords And Decrease Damage";
        public string Description => "새로운 검들을 추가하지만 대미지가 약해집니다";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.IncreaseWeaponCount(2);
            components.Player.ChangeAttackDamage("+", -30);
        }
    }
}