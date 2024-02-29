using System.Collections.Generic;
using System.Collections.ObjectModel;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.StatModifiers;

namespace Code.Runtime.Entity.StatusEffects
{
    public class GameplayEffect
    {
        public EffectDurationType Type { get; private set; }
        public ReadOnlyCollection<StatModifier> Modifiers => _statModifiers.AsReadOnly();
        public StatusVisualEffect StatusVisualEffect => _statusVisualEffect;
        
        public List<StatModifierBlueprint> ModifierTemplates;
        
        private readonly List<StatModifier> _statModifiers = new List<StatModifier>();
        private StatusVisualEffect _statusVisualEffect;

        public GameplayEffect(List<StatModifierBlueprint> modifiers, EffectDurationType type, 
            StatusVisualEffect statusVisualEffect)
        {
            Type = type;
            ModifierTemplates = modifiers;
            _statusVisualEffect = statusVisualEffect;
            
            StatModifier statModifier;

            foreach (var modifier in modifiers)
            {
                if (modifier is DamageStatModifierBlueprint)
                {
                    var healthModifier = new HealthModifier
                    {
                        Magnitude = modifier.Magnitude * -1,
                        OperationType = modifier.Type
                    };

                    statModifier = healthModifier;
                }
                else
                {
                    statModifier = new StatModifier()
                    {
                        Magnitude = modifier.Magnitude
                    };
                }

                statModifier.Source = this;
                _statModifiers.Add(statModifier);
            }
            
        }
    }
}
