using TowerDefence.Game.Shared;

namespace TowerDefence.Game.Stats.Contract
{
    public interface IBuffTarget
    {
        public BuffDeBuffVariant GetDeBuffVariant { get; }
        
        public float Value { get; }
    }
}