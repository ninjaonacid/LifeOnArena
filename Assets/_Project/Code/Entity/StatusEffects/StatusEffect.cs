using System.Collections.Generic;
using Code.ConfigData.StatSystem;
using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public abstract class StatusEffect
    {
        public List<StatModifier> Modifiers { get; private set; }

        protected StatusEffect(List<StatModifier> modifiers)
        {
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
