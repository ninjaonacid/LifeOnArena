using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public abstract class StatusEffect
    {
        public float Duration { get; private set; }
        public float RemainingDuration { get; private set; }
        public abstract void Apply(GameObject target);

        public StatusEffect TickEffect(float deltaTime)
        {
            RemainingDuration -= deltaTime;

            if (RemainingDuration <= 0)
            {
                
            }

            return this;
        }
    }
}
