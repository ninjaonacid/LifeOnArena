using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public class StunStatusEffect : IStatusEffect
    {
        private readonly float _duration;
        public StunStatusEffect(float duration)
        {
            _duration = duration;
        }
        public void Apply(GameObject target)
        {
            var statusController = target.GetComponent<StatusEffectController>();
            statusController.AddEffect(this);
        }
    }
}