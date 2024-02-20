using System.Collections.Generic;
using Code.ConfigData.StatusEffects;
using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public class StunStatusEffect : DurationalStatusEffect
    {
        public StunStatusEffect(List<StatModifierTemplate> modifiers, EffectDurationType type, float duration, float remainingDuration, float tickRate) : base(modifiers, type, duration, remainingDuration, tickRate)
        {
        }

        public override void Apply(GameObject target)
        {
            var statusController = target.GetComponent<StatusEffectController>();
            statusController.ApplyEffectToSelf(this);
        }
    }
}