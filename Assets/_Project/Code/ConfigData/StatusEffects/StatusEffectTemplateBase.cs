using System.Collections.Generic;
using Code.ConfigData.Identifiers;
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
        public string Name;
        public string Description;
        public StatusEffectId StatusEffectId;
        public EffectDurationType EffectDurationType;
        public List<StatModifierTemplate> Modifiers;

        public abstract StatusEffect GetStatusEffect();
    }
}