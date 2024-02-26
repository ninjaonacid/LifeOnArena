using System.Collections.Generic;
using System.Collections.ObjectModel;
using Code.Runtime.ConfigData.StatSystem;
using Code.Runtime.ConfigData.StatSystem.StatModifiers;
using Code.Runtime.ConfigData.StatusEffects;

namespace Code.Runtime.Entity.StatusEffects
{
    public class GameplayEffect
    {
        public EffectDurationType Type { get; private set; }

        private readonly List<StatModifier> _statModifiers = new List<StatModifier>();
        public ReadOnlyCollection<StatModifier> Modifiers => _statModifiers.AsReadOnly();
        public List<StatModifierTemplate> ModifierTemplates;

        public GameplayEffect(List<StatModifierTemplate> modifiers, EffectDurationType type)
        {
            Type = type;
            ModifierTemplates = modifiers;
            StatModifier statModifier;

            foreach (var modifier in modifiers)
            {
                if (modifier is DamageStatModifierTemplate)
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
