using System.Linq;
using Code.Logic.Damage;

namespace Code.StaticData.StatSystem
{
    public class Health : Attribute
    {
        public Health(StatDefinition statDefinition, StatController statController) : base(statDefinition, statController)
        {
        }
        
        public void ResetHealth()
        {
            _currentValue = _value;
            HealthChanged();
        }
        
        public override void ApplyModifier(StatModifier modifier)
        {
            base.ApplyModifier(modifier);
            
            var damageSource = modifier.Source as IDamageSource;
            
            if (damageSource == null) return;
            
            if (damageSource.DamageTypes.Contains(DamageType.Physical))
            {
                modifier.Magnitude += _statController.Stats["Defense"].Value;
            } 
            else if (damageSource.DamageTypes.Contains(DamageType.Magical))
            {
                
            }
        }
    }
}
