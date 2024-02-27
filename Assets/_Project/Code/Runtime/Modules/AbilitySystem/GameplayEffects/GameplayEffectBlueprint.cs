﻿using System.Collections.Generic;
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
        public EffectDurationType EffectDurationType;
        public List<StatModifierBlueprint> Modifiers;
        public List<GameplayTag> Tags;
        
        public virtual GameplayEffect GetGameplayEffect()
        {
            return new GameplayEffect(Modifiers, EffectDurationType);
        }
        
    }
}