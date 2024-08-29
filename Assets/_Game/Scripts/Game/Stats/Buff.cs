using System;
using TowerDefence.Game.Shared;
using TowerDefence.Game.Stats.Contract;
using TowerDefence.Systems.Shared;

namespace TowerDefence.Game.Stats
{
    public abstract class Buff
    {
        public BuffDeBuffVariant BuffVariant { get; }

        protected IntervalTimer Timer = new ();
        
        protected float Duration;
        protected float Step;

        public abstract void Apply(IBuffTarget target, Action<Buff> callback);

        public virtual void Cancel() => Timer.Cancel();
    }
}