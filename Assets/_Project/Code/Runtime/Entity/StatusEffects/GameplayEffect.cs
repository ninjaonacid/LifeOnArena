using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Code.Runtime.ConfigData;
using Code.Runtime.Logic.Damage;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Code.Runtime.Modules.AbilitySystem.GameplayTags;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.StatModifiers;
using UnityEngine;


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
        private readonly AbilityController _owner;
        public GameplayEffect(GameplayEffectBlueprint effectBlueprint, AbilityController owner)
        {
         
            _owner = owner;
            ModifierBlueprints = effectBlueprint.Modifiers;
            EffectTags = effectBlueprint.Tags;
            Type = effectBlueprint.DurationType;
            StatusVisualEffect = effectBlueprint.StatusVisualEffect;


            StatController ownerStats = _owner.GetComponent<StatController>();
            
            ownerStats.Stats.TryGetValue("Attack", out var ownerPhysicalDamage);
            ownerStats.Stats.TryGetValue("Magic", out var ownerMagicalDamage);
            

            StatModifier statModifier;

            foreach (var modifier in effectBlueprint.Modifiers)
            {
                if (modifier is DamageStatModifierBlueprint damageModifier)
                {
                    HealthModifier healthModifier = default;
                    
                    switch (damageModifier.DamageType)
                    {
                        case DamageType.Magical:
                        {
                            if (ownerMagicalDamage != null)
                                healthModifier = new HealthModifier
                                {
                                    Magnitude = Mathf.RoundToInt(ownerMagicalDamage.Value * damageModifier.ScaleFactor) * -1,
                                    OperationType = damageModifier.Type
                                };
                            break;
                        }
                        case DamageType.Physical:
                        {
                            if (ownerPhysicalDamage != null)
                                healthModifier = new HealthModifier
                                {
                                    Magnitude = Mathf.RoundToInt(ownerPhysicalDamage.Value * damageModifier.ScaleFactor) * -1,
                                    OperationType = damageModifier.Type
                                };
                            break;
                        }
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    

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
