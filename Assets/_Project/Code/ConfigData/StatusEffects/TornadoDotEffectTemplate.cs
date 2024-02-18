using Code.Entity.StatusEffects;

namespace Code.ConfigData.StatusEffects
{
    public class TornadoDotEffectTemplate : StatusEffectTemplate<TornadoDotEffect>
    {
        public float _damageTick;
        public override StatusEffect GetStatusEffect()
        {
            return new TornadoDotEffect(Duration, _damageTick);
        }
    }
}
