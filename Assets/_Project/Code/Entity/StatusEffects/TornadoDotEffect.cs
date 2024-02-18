using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public class TornadoDotEffect : DamageOverTimeEffect
    {
        public TornadoDotEffect(float duration, float damageTick) : base(duration, damageTick)
        {
        }

        public override void Apply(GameObject target)
        {
            var statusController = target.GetComponent<StatusEffectController>();
            statusController.AddEffect(this);
            
        }
    }
}
