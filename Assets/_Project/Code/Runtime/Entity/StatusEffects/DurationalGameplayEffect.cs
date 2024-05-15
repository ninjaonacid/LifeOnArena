using System.Collections.Generic;
using System.Collections.ObjectModel;
using Code.Runtime.ConfigData;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Code.Runtime.Modules.AbilitySystem.GameplayTags;
using UnityEngine;

namespace Code.Runtime.Entity.StatusEffects
{
    public class DurationalGameplayEffect : GameplayEffect
    {
        private float _remainingDuration;
        private float _remainingToExecute;

        private readonly float _duration;
        private readonly float _executeRate;


        public DurationalGameplayEffect(GameplayEffectBlueprint effectBlueprint, AbilityController owner, float duration, float executeRate) : base(effectBlueprint, owner)
        {
            _remainingDuration = duration;
            _duration = duration;
            _executeRate = executeRate;
        }

        public DurationalGameplayEffect TickEffect(float deltaTime)
        {
            _remainingDuration = Mathf.Max(_remainingDuration - deltaTime, 0f);
            _remainingToExecute = Mathf.Max(_remainingToExecute - deltaTime, 0f);

            return this;
        }

        public void ResetExecutionTime()
        {
            _remainingToExecute = _executeRate;
        }

        public void ResetRemainingDuration()
        {
            _remainingDuration = _duration;
        }

        public bool IsExecuteTime() => Mathf.Approximately(_remainingToExecute, 0f);

        public bool IsDurationEnd() => Mathf.Approximately(_remainingDuration, 0f);
    }
}