using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public class StunStatusEffect : IStatusEffect
    {
        public void Apply(GameObject target)
        {
            var statusController = target.GetComponent<StatusEffectController>();
            statusController.AddEffect(this);
        }
    }
}