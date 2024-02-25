using Code.Entity.StatusEffects;
using UnityEngine;

namespace Code.ConfigData.StatusEffects
{
    [CreateAssetMenu(fileName = "DamageStatusEffect", menuName = "Config/StatusEffect/DamageStatusEffect")]
    public class DamageEffectTemplate : StatusEffectTemplate<DamageEffect>
    {
        public override StatusEffect GetStatusEffect()
        {
            return new DamageEffect(Modifiers, EffectDurationType);
        }
    }
}