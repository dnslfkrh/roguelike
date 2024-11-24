namespace Upgrades.Movement
{
    using UnityEngine;
    using Upgrades.Core;

    public class PlayerSpeedUpgrade : IUpgrade
    {
        public string Name => "Player Speed +";
        public string Description => "�÷��̾��� �̵��ӵ��� ������ŵ�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(2);
        }
    }

    public class PlayerAndWeaponSpeedUpgrade : IUpgrade
    {
        public string Name => "Player And Weapon Speed Upgrade";
        public string Description => "�÷��̾��� �̵� �ӵ��� ������ ȸ�� �ӵ��� �����մϴ�.";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(1);
            components.WeaponManager.ChangeRotationSpeed("+", 30);
        }
    }
}