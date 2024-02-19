using Code.Entity.StatusEffects;

namespace Code.ConfigData.StatusEffects
{
    public abstract class DurationalStatusEffectTemplate<T> : StatusEffectTemplateBase where T : DurationalStatusEffect
    {
        public float Duration;
        public float TickRate;
    }
}