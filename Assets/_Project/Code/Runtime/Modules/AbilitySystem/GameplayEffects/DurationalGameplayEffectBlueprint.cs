using Code.Runtime.Entity.StatusEffects;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.GameplayEffects
{
    [CreateAssetMenu(menuName = "AbilitySystem/GameplayEffect/DurationalGameplayEffect", fileName = "DurationalGameplayEffect")]
    public class DurationalGameplayEffectBlueprint : GameplayEffectBlueprint
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _tickRate;
        public float Duration => _duration;
        public float TickRate => _tickRate;
        public override GameplayEffect GetGameplayEffect()
        {
            return new DurationalGameplayEffect(Modifiers, EffectDurationType, Duration, Duration, TickRate);
        }
    }
}