using Code.Entity.StatusEffects;
using UnityEngine;

namespace Code.ConfigData.StatusEffects
{
    [CreateAssetMenu(fileName = "StunStatusEffect", menuName = "Config/StatusEffect/StunStatusEffect")]
    public class StunStatusEffectTemplate : DurationalStatusEffectTemplate<StunStatusEffect>
    {
        private StatusEffect _statusEffect;
        
        public override StatusEffect GetStatusEffect()
        {
            // _statusEffect ??= new StunStatusEffect(Modifiers., EffectDurationType, Duration, Duration, TickRate);
            // return _statusEffect;
            return null;
        }
    }
}
