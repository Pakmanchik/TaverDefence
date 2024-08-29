using Attribute = TowerDefence.Game.Entity.Stats.Attribute;

namespace TowerDefence.Game.Shared
{
    /// <summary>
    /// Формула эффективности брони: <code>Damage Reduction = Armor / 1 + 0.06 * Armor</code>
    /// Формула расчета фактического урона: <code>Base Damage × (1 − Damage Reduction)</code>
    /// </summary>
    public sealed class DamageReceiver 
    {
        public void TakeDamage(float damage, Attribute armor, Attribute currentHealth)
        {
            var damageReduction = armor.Amount / (1 + 0.06f * armor.Amount);
            var effectiveDamage = damage * (1 - damageReduction);
            
            currentHealth.Amount -= effectiveDamage;
        }
    }
}