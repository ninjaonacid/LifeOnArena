using System.Collections.Generic;
using Code.Entity.StatusEffects;
using UnityEngine;

namespace Code.ConfigData.StatusEffects
{
    public enum EffectDurationType
    {
        Instant = 1,
        HasDuration = 2
    }
    public abstract class StatusEffectTemplateBase : ScriptableObject
    {
        public EffectDurationType EffectDurationType;
        public List<StatModifierTemplate> Modifiers;

        public abstract StatusEffect GetStatusEffect();
    }
}