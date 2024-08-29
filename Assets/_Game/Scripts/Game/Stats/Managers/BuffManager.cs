using System.Collections.Generic;
using System.ComponentModel;
using TowerDefence.Game.Shared;
using TowerDefence.Game.Stats.Contract;

namespace TowerDefence.Game.Stats.Managers
{
    public sealed class BuffManager : IBuffManager
    {
        private readonly Dictionary<BuffDeBuffVariant, IBuffTarget> _targets = new();
        private readonly List<Buff> _buffs = new();

        public void SetBuffTargets(List<IBuffTarget> buffTargets)
        {
            foreach (var target in buffTargets)
                _targets.Add(target.GetDeBuffVariant, target);
        }

        public void SetBuff(Buff buff)
        {
            if (buff == null)
                throw new WarningException("buff cannot be null in SetBuff");

            if (!_targets.TryGetValue(buff.BuffVariant,out var value))
                return;
            
            buff.Apply(value, RemoveBuff);
            _buffs.Add(buff);
        }

        private void RemoveBuff(Buff buff)
        {
            if (buff == null)
                throw new WarningException("buff cannot be null in RemoveBuff");

            if (!_buffs.Contains(buff))
                throw new WarningException("The specified buff is not in the list");

            _buffs.Remove(buff);
        }

        public void RemoveAllBuffs()
        {
            foreach (var buff in _buffs)
                buff.Cancel();
            
            _buffs.Clear();
        }
    }
}