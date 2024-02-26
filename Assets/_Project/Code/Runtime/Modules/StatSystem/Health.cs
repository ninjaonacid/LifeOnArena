namespace Code.Runtime.Modules.StatSystem
{
    public class Health : Attribute
    {
        public Health(StatDefinition statDefinition, StatController statController) : base(statDefinition, statController)
        {
        }
        
        public void ResetHealth()
        {
            _currentValue = _value;
            AttributeValueChanged();
        }
        
        public override void ApplyModifier(StatModifier modifier)
        {
            base.ApplyModifier(modifier);
            
            // var damageSource = modifier.Source as IDamageSource;
            //
            // if (damageSource == null) return;
            //
            // if (damageSource.DamageTypes.Contains(DamageType.Physical))
            // {
            //     modifier.Magnitude += _statController.Stats["Defense"].Value;
            // } 
            // else if (damageSource.DamageTypes.Contains(DamageType.Magical))
            // {
            //     
            // }
        }
    }
}
