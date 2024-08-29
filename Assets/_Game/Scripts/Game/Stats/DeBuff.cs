using TowerDefence.Game.Shared;
using TowerDefence.Game.Stats.Contract;
using TowerDefence.Systems.Shared;

namespace TowerDefence.Game.Stats
{
    public abstract class DeBuff
    {
        public BuffDeBuffVariant DeBuffVariant { get; }
        
        protected IntervalTimer Timer = new ();
        
        protected float Duration;
        protected float Step;

        public abstract void Apply(IDeBuffTarget target);

        public virtual void Cancel() => Timer.Cancel();
    }
}