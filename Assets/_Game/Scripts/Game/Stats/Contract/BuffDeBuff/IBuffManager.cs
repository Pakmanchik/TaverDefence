using System.Collections.Generic;

namespace TowerDefence.Game.Stats.Contract
{
    public interface IBuffManager
    {
        public void SetBuffTargets(List<IBuffTarget> buffTargets);
        public void SetBuff(Buff buff);
        public void RemoveAllBuffs();
    }
}