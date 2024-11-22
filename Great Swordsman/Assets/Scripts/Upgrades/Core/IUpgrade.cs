namespace Upgrades.Core
{
    public interface IUpgrade
    {
        string Name { get; }
        string Description { get; }
        void Apply(PlayerComponents components);
    }
}