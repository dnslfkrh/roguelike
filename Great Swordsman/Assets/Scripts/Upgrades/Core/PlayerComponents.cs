namespace Upgrades.Core
{
    public struct PlayerComponents
    {
        public Player Player;
        public PlayerHP PlayerHP;
        public WeaponManager WeaponManager;

        public PlayerComponents(Player player, PlayerHP playerHP, WeaponManager weaponManager)
        {
            Player = player;
            PlayerHP = playerHP;
            WeaponManager = weaponManager;
        }
    }
}