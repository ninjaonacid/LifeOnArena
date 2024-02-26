using Code.Runtime.Entity.StatusEffects;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.GameplayEffects
{
    [CreateAssetMenu(menuName = "AbilitySystem/GameplayEffect/GameplayEffect", fileName = "GameplayEffect")]
    public class DurationalGameplayEffectBlueprint : GameplayEffectBlueprint
    {
        public float Duration;
        public float TickRate;
        public bool IsDisablingMovement;
        public override GameplayEffect GetGameplayEffect()
        {
            return new DurationalGameplayEffect(Modifiers, EffectDurationType, Duration, Duration, TickRate,
                IsDisablingMovement);
        }
    }
}