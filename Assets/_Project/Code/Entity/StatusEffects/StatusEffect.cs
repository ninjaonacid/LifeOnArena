using System.Collections.Generic;
using Code.ConfigData.StatSystem;
using Code.ConfigData.StatusEffects;
using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public abstract class StatusEffect
    {
        public EffectDurationType Type { get; private set; }
        public List<StatModifier> Modifiers { get; private set; }

        protected StatusEffect(List<StatModifier> modifiers, EffectDurationType type)
        {
            this.Type = type;
            Modifiers = modifiers;
        }
        
        public abstract void Apply(GameObject target);

        // public StatusEffect TickEffect(float deltaTime)
        // {
        //     RemainingDuration -= deltaTime;
        //
        //     if (RemainingDuration <= 0)
        //     {
        //         
        //     }
        //
        //     return this;
        // }
    }
}
