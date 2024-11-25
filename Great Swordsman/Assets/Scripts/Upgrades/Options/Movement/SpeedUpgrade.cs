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

    public class IncreasePlayerSpeedAndDecreaseMaxHPUpgrade : IUpgrade
    {
        public string Name => "Increase Player Speed And Decrease MaxHP Upgrade";
        public string Description => "�÷��̾��� �̵� �ӵ��� �����ϰ� �ִ� ü���� �����մϴ�.";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(1);
            components.PlayerHP.ChangeHP("maxHP", -200);
        }
    }

    public class DecreasePlayerSpeedAndIncreaseMaxHPUpgrade : IUpgrade
    {
        public string Name => "Decrease Player Speed And Increase MaxHP Upgrade";
        public string Description => "�÷��̾��� �̵� �ӵ��� �����ϰ� �ִ� ü���� �����մϴ�.";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(-1);
            components.PlayerHP.ChangeHP("maxHP", 200);
        }
    }
}