using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public abstract class DamageOverTimeEffect : StatusEffect
    {
        private float _duration;
        private float _damageTick;

        protected DamageOverTimeEffect(float duration, float damageTick)
        {
            _duration = duration;
            _damageTick = damageTick;
        }
    }
}
