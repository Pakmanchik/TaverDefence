using System;
using TowerDefence.Game.Shared;
using TowerDefence.Game.Stats.Contract;

namespace TowerDefence.Game.Entity.Stats
{
    public sealed class Attribute : IBuffTarget
    {
        public event Action<Attribute> OnValueChanged;
        
        private float _amount;

        public float Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnValueChanged?.Invoke(this);
            }
        }

        public BuffDeBuffVariant GetDeBuffVariant { get; set; }
        public float Value => Amount;
    }
}