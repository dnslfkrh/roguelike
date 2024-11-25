namespace Upgrades.Weapons
{
    using Upgrades.Core;

    public class AddSwordUpgrade : IUpgrade
    {
        public string Name => "Add Sword";
        public string Description => "���ο� ���� �߰��մϴ�";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.IncreaseWeaponCount(1);
        }
    }

    public class WeaponRotationSpeedUpgrade : IUpgrade
    {
        public string Name => "Increase Weapon Rotation Speed";
        public string Description => "���� ȸ�� �ӵ��� ������ŵ�ϴ�";

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
        public string Description => $"������ ȸ�� �ݰ��� {(direction == "Increase" ? "����" : "����")}��ŵ�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.ChangeDistanceFromPlayer(distanceChange);
        }
    }

    public class AddSwordsAndDecreaseDamageUpgrade: IUpgrade
    {
        public string Name => "Add Swords And Decrease Damage";
        public string Description => "���ο� �˵��� �߰������� ������� �������ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.WeaponManager.IncreaseWeaponCount(2);
            components.Player.ChangeAttackDamage("+", -30);
        }
    }
}