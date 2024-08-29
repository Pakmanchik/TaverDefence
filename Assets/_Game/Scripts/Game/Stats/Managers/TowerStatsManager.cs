using TowerDefence.Game.Entity.Stats;
using TowerDefence.Game.Shared;

namespace TowerDefence.Game.Stats.Managers
{
    public sealed class TowerStatsManager
    {
        public TowerStatsManager(float maxHealth, float armor, float attackRate, float attackRange)
        {
            CurrentHealth.Amount = maxHealth;
            MaxHealth.Amount     = maxHealth;
            Armor.Amount         = armor;
            AttackRate.Amount    = attackRate;
            AttackRange.Amount   = attackRange;

            MaxHealth.GetDeBuffVariant   = BuffDeBuffVariant.Health;
            Armor.GetDeBuffVariant       = BuffDeBuffVariant.Armor;
            AttackRate.GetDeBuffVariant  = BuffDeBuffVariant.AttackRate;
            AttackRange.GetDeBuffVariant = BuffDeBuffVariant.AttackRange;
        }

        public Attribute CurrentHealth { get; } = new();
        public Attribute MaxHealth { get; } = new();
        public Attribute Armor { get; } = new();

        public Attribute AttackRate { get; } = new();
        public Attribute AttackRange { get; } = new();
    }
}