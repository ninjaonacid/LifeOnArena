using System.Collections.Generic;
using Code.ConfigData.StatusEffects;
using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public class DurationalGameplayEffect : GameplayEffect
    {
        private float _remainingDuration;
        private float _remainingToExecute;

        private readonly float _duration;
        private readonly float _executeRate;
        public bool IsDisablingEffect { get; private set; }

        public DurationalGameplayEffect(List<StatModifierTemplate> modifiers, EffectDurationType type, float duration, float remainingDuration, float executeRate, bool isDisablingEffect) : base(modifiers, type)
        {
            _duration = duration;
            _executeRate = executeRate;
            IsDisablingEffect = isDisablingEffect;
            _remainingDuration = remainingDuration;
        }
        
        public DurationalGameplayEffect TickEffect(float deltaTime)
        {
            _remainingDuration = Mathf.Max(_remainingToExecute - deltaTime, 0f);
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
