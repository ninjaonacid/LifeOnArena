using System.Collections.Generic;
using Code.ConfigData.StatSystem;
using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public class StunStatusEffect : StatusEffect
    {
       

        public override void Apply(GameObject target)
        {
            var statusController = target.GetComponent<StatusEffectController>();
            statusController.ApplyEffectToSelf(this);
        }

        public StunStatusEffect(List<StatModifier> modifiers) : base(modifiers)
        {
        }
    }
}