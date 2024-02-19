using System.Collections.Generic;
using Code.ConfigData.StatSystem;
using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public abstract class DurationalStatusEffect : StatusEffect
    {
        public float Duration;
        public float RemainingDuration;
        public float TickRate;

        protected DurationalStatusEffect(List<StatModifier> modifiers, float duration, float remainingDuration) : base(modifiers)
        {
            Duration = duration;
            RemainingDuration = remainingDuration;
        }
    }
}
