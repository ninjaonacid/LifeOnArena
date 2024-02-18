using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public class StunStatusEffect : DisablingStatusEffect
    {
        public StunStatusEffect(float duration) : base(duration)
        {
        }

        public override void Apply(GameObject target)
        {
            var statusController = target.GetComponent<StatusEffectController>();
            statusController.ApplyEffectToSelf(this);
        }
    }
}