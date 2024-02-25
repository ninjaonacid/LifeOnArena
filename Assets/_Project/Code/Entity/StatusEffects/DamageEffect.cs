using System.Collections.Generic;
using Code.ConfigData.StatusEffects;
using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public class DamageEffect : StatusEffect
    {
        public DamageEffect(List<StatModifierTemplate> modifiers, EffectDurationType type) : base(modifiers, type)
        {
        }

        public override void Apply(GameObject target)
        {
        }
    }
}
