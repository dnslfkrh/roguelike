namespace Upgrades.Movement
{
    using Upgrades.Core;

    public class PlayerSpeedUpgrade : IUpgrade
    {
        public string Name => "Player Speed +";
        public string Description => "�÷��̾��� �̵��ӵ��� ������ŵ�ϴ�";

        public void Apply(PlayerComponents components)
        {
            components.Player.IncreaseMoveSpeed(1);
        }
    }
}