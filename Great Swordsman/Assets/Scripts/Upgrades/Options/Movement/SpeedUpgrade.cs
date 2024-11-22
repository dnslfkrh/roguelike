namespace Upgrades.Movement
{
    using Upgrades.Core;

    public class PlayerSpeedUpgrade : IUpgrade
    {
        public string Name => "Player Speed +";
        public string Description => "플레이어의 이동속도를 증가시킵니다";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(1);
        }
    }
}