using TowerDefence.Game.Shared;
using TowerDefence.Game.Stats;
using TowerDefence.Game.Stats.Contract;
using TowerDefence.Game.Stats.Managers;
using UnityEngine;

namespace TowerDefence.Game.Entity.Towers
{
    public class AbstractNativeTower
    {
        protected readonly TowerStatsManager Stats;
        private readonly IBuffManager _buffManager;
        private readonly DamageReceiver _damageReceiver;

        protected AbstractTower TowerView;

        public Transform Transform => TowerView.gameObject.transform;

        public AbstractNativeTower(TowerStatsManager towerStatsManager,
                                   AbstractTower tower,
                                   IBuffManager buffManager,
                                   DamageReceiver damageReceiver)
        {
            TowerView       = tower;
            Stats           = towerStatsManager;
            _buffManager    = buffManager;
            _damageReceiver = damageReceiver;
        }

        public void InitElements()
        {
            TowerView.Init(CalculateDamage, AddBuff);
        }

        private void CalculateDamage(float damage) =>
            _damageReceiver.TakeDamage(damage, Stats.Armor, Stats.CurrentHealth);
        
        private void AddBuff(Buff buff) => _buffManager.SetBuff(buff);
    }
}