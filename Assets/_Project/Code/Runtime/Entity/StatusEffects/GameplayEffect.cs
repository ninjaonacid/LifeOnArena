using System.Collections.Generic;
using System.Collections.ObjectModel;
using Code.Runtime.ConfigData;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Code.Runtime.Modules.AbilitySystem.GameplayTags;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.StatModifiers;

namespace Code.Runtime.Entity.StatusEffects
{
    public class GameplayEffect
    {
        public EffectDurationType Type { get; private set; }
        public readonly ReadOnlyCollection<GameplayTag> EffectTags;
        public ReadOnlyCollection<StatModifier> Modifiers => _statModifiers.AsReadOnly();
        public readonly ReadOnlyCollection<StatModifierBlueprint> ModifierBlueprints;
        public StatusVisualEffect StatusVisualEffect { get; }
        
        private readonly List<StatModifier> _statModifiers = new List<StatModifier>();
        private AbilityController _owner;
        public GameplayEffect(GameplayEffectBlueprint effectBlueprint, AbilityController owner)
        {
         
            _owner = owner;
            ModifierBlueprints = effectBlueprint.Modifiers;
            EffectTags = effectBlueprint.Tags;
            Type = effectBlueprint.DurationType;
            StatusVisualEffect = effectBlueprint.StatusVisualEffect;

            Stat ownerDamage;
            _owner.GetComponent<StatController>().Stats.TryGetValue("Attack", out ownerDamage);
            
            StatModifier statModifier;

            foreach (var modifier in effectBlueprint.Modifiers)
            {
                if (modifier is DamageStatModifierBlueprint)
                {
                    var healthModifier = new HealthModifier
                    {
                        Magnitude = ownerDamage.Value * -1,
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
