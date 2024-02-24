using Code.Entity.StatusEffects;

namespace Code.ConfigData.StatusEffects
{
    public class DamageEffectTemplate : StatusEffectTemplate<DamageEffect>
    {
        public override StatusEffect GetStatusEffect()
        {
            return new DamageEffect(Modifiers, EffectDurationType);
        }
    }
}