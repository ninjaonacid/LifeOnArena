using Code.ConfigData.Identifiers;
using Code.Entity.StatusEffects;
using UnityEngine;

namespace Code.ConfigData.StatusEffect
{
    public abstract class StatusEffectTemplateBase : ScriptableObject
    {
        public string Name;
        public string Description;
        public StatusEffectId StatusEffectId;
        public float Duration;

        public abstract IStatusEffect GetStatusEffect();
    }
}