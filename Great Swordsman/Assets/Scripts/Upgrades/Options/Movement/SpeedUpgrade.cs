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

    public class PlayerSpeedAndMaxHPUpgrade : IUpgrade
    {
        public string Name => "Player Speed And MaxHP Upgrade";
        public string Description => "플레이어의 이동 속도와 최대 체력이 증가합니다.";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(1);
            components.PlayerHP.IncreaseHP("maxHP", 150);
        }
    }
}