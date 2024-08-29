using TowerDefence.Game.Shared;

namespace TowerDefence.Game.Stats.Contract
{
    public interface IDeBuffTarget
    {
        public BuffDeBuffVariant GetDeBuffVariant { get; }
    }
}