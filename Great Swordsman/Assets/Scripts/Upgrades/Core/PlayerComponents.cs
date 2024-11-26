namespace Upgrades.Core
{
    public struct PlayerComponents
    {
        public Player Player;
        public PlayerHP PlayerHP;
        public WeaponManager WeaponManager;
        public SkillManager SkillManager;

        public PlayerComponents(Player player, PlayerHP playerHP, WeaponManager weaponManager, SkillManager skillManager)
        {
            Player = player;
            PlayerHP = playerHP;
            WeaponManager = weaponManager;
            SkillManager = skillManager;
        }
    }
}