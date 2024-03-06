using System.Collections.Generic;
using System.Collections.ObjectModel;
using Code.Runtime.ConfigData;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Code.Runtime.Modules.AbilitySystem.GameplayTags;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.StatModifiers;

namespace Code.Runtime.Entity.StatusEffects
{
    public class GameplayEffect
    {
        public EffectDurationType Type { get; private set; }
        public ReadOnlyCollection<GameplayTag> EffectTags;
        public ReadOnlyCollection<StatModifier> Modifiers => _statModifiers.AsReadOnly();
        public readonly ReadOnlyCollection<StatModifierBlueprint> ModifierBlueprints;
        public StatusVisualEffect StatusVisualEffect { get; }
        
        private readonly List<StatModifier> _statModifiers = new List<StatModifier>();
        public GameplayEffect(ReadOnlyCollection<StatModifierBlueprint> modifiers, ReadOnlyCollection<GameplayTag> tags, EffectDurationType type, 
            StatusVisualEffect statusVisualEffect)
        {
            ModifierBlueprints = modifiers;
            EffectTags = tags;
            Type = type;
            StatusVisualEffect = statusVisualEffect;
            
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
