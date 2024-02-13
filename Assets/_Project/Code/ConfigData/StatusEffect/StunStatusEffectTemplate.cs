using Code.Entity.StatusEffects;

namespace Code.ConfigData.StatusEffect
{
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
