using Code.Entity.StatusEffects;
using UnityEngine;

namespace Code.ConfigData.StatusEffect
{
    [CreateAssetMenu(fileName = "StunStatusEffect", menuName = "Config/StatusEffect/StunStatusEffect")]
    public class StunStatusEffectTemplate : StatusEffectTemplate<StunStatusEffect>
    {
        
        private IStatusEffect _statusEffect;
        
        public override IStatusEffect GetStatusEffect()
        {
            _statusEffect ??= new StunStatusEffect();
            return _statusEffect;
        }
    }
}
