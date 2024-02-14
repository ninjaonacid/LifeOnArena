using Code.ConfigData.Identifiers;
using Code.Entity.StatusEffects;
using UnityEngine;

namespace Code.ConfigData.StatusEffect
{
    public enum EffectDuration
    {
        Instant = 1,
        HasDuration = 2
    }
    public abstract class StatusEffectTemplateBase : ScriptableObject
    {
        public string Name;
        public string Description;
        public StatusEffectId StatusEffectId;
        public EffectDuration EffectDuration;
        public float Duration;

        public abstract IStatusEffect GetStatusEffect();
    }
}