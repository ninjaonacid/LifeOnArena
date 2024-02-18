using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public abstract class DisablingStatusEffect : StatusEffect
    {
        private float _duration;
        public float Duration => _duration;
        protected DisablingStatusEffect(float duration)
        {
            _duration = duration;
        }

    }
}
