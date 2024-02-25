using System.Collections.Generic;
using Code.ConfigData.StatusEffects;
using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public class StunStatusEffect : DurationalStatusEffect
    {
        public StunStatusEffect(List<StatModifierTemplate> modifiers, EffectDurationType type, float duration, float remainingDuration, float executeRate, bool isDisablingEffect) : base(modifiers, type, duration, remainingDuration, executeRate, isDisablingEffect)
        {
        }

        public override void Apply(GameObject target)
        {
            
        }
    }
}