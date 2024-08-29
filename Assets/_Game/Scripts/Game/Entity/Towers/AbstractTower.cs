using System;
using TowerDefence.Game.Stats;
using TowerDefence.Game.Stats.Contract;
using UnityEngine;

namespace TowerDefence.Game.Entity.Towers
{
    public class AbstractTower : MonoBehaviour, ITakeDamage, IBuffable
    {
        private Action<float> OnDamageTaken;
        private Action<float> OnHealTaken;
        private Action<Buff> OnBuff;
        
        public void Init(Action<float> onDamageTaken, Action<Buff> onBuff)
        {
            OnDamageTaken = onDamageTaken;
            OnBuff        = onBuff;
        }

        public void TakeDamage(int damage) => OnDamageTaken?.Invoke(damage);

        public void SetBuff(Buff buff) => OnBuff?.Invoke(buff);

      
    }
}