using System.Collections.Generic;
using Code.ConfigData.StatusEffects;

namespace Code.Entity.StatusEffects
{
    public abstract class DurationalStatusEffect : StatusEffect
    {
        public float Duration;
        public float RemainingDuration;
        public float TickRate;


        protected DurationalStatusEffect(List<StatModifierTemplate> modifiers, EffectDurationType type, float duration, float remainingDuration, float tickRate) : base(modifiers, type)
        {
            Duration = duration;
            RemainingDuration = remainingDuration;
            TickRate = tickRate;
        }
    }
}
