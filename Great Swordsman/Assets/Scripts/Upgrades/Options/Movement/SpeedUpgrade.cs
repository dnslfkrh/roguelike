namespace Upgrades.Movement
{
    using UnityEngine;
    using Upgrades.Core;

    public class PlayerSpeedUpgrade : IUpgrade
    {
        public string Name => "Player Speed +";
        public string Description => "플레이어의 이동속도를 증가시킵니다";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(2);
        }
    }

    public class PlayerAndWeaponSpeedUpgrade : IUpgrade
    {
        public string Name => "Player And Weapon Speed Upgrade";
        public string Description => "플레이어의 이동 속도와 무기의 회전 속도가 증가합니다.";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(1);
            components.WeaponManager.ChangeRotationSpeed("+", 30);
        }
    }

    public class IncreasePlayerSpeedAndDecreaseMaxHPUpgrade : IUpgrade
    {
        public string Name => "Increase Player Speed And Decrease MaxHP Upgrade";
        public string Description => "플레이어의 이동 속도가 증가하고 최대 체력이 감소합니다.";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(1);
            components.PlayerHP.ChangeHP("maxHP", -200);
        }
    }

    public class DecreasePlayerSpeedAndIncreaseMaxHPUpgrade : IUpgrade
    {
        public string Name => "Decrease Player Speed And Increase MaxHP Upgrade";
        public string Description => "플레이어의 이동 속도가 감소하고 최대 체력이 증가합니다.";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(-1);
            components.PlayerHP.ChangeHP("maxHP", 200);
        }
    }
}