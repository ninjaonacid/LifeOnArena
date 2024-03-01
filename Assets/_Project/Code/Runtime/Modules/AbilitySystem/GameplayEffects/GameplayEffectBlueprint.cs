using System.Collections.Generic;
using System.Collections.ObjectModel;
using Code.Runtime.ConfigData;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Modules.AbilitySystem.GameplayTags;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.GameplayEffects
{
    public enum EffectDurationType
    {
        Instant = 1,
        HasDuration = 2
    }
    
    [CreateAssetMenu(menuName = "AbilitySystem/GameplayEffect/GameplayEffect", fileName = "GameplayEffect")]
    public class GameplayEffectBlueprint : ScriptableObject
    {
        [SerializeField] private EffectDurationType _effectDurationType;
        [SerializeField] private List<StatModifierBlueprint> _modifiers;
        [SerializeField] private List<GameplayTag> _tags;
        [SerializeField] private StatusVisualEffect _statusVisualEffect;
        
        public EffectDurationType DurationType => _effectDurationType;
        public ReadOnlyCollection<StatModifierBlueprint> Modifiers => _modifiers.AsReadOnly();
        public ReadOnlyCollection<GameplayTag> Tags => _tags.AsReadOnly();
        public StatusVisualEffect StatusVisualEffect => _statusVisualEffect;
        
        public virtual GameplayEffect GetGameplayEffect()
        {
            return new GameplayEffect(Modifiers, Tags, DurationType, StatusVisualEffect);
        }

    }
}