using System.Collections.Generic;
using Code.ConfigData.StatusEffects;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace Code.Entity.StatusEffects
{
    public abstract class DurationalStatusEffect : StatusEffect
    {
        public float RemainingDuration { get; set; }
        public float RemainingToExecute { get; set; }

        private readonly float _duration;
        private float _executeRate;
        
        public bool IsDisablingEffect { get; private set; }


        protected DurationalStatusEffect(List<StatModifierTemplate> modifiers, EffectDurationType type, float duration, float remainingDuration, float executeRate, bool isDisablingEffect) : base(modifiers, type)
        {
            _duration = duration;
            _executeRate = executeRate;
            IsDisablingEffect = isDisablingEffect;
            RemainingDuration = remainingDuration;
        }
        
        public DurationalStatusEffect TickEffect(float deltaTime)
        {
            RemainingDuration = Mathf.Max(RemainingToExecute - deltaTime, 0f);
            RemainingToExecute = Mathf.Max(RemainingToExecute - deltaTime, 0f);
            
            return this;
        }

        public void ResetExecutionTime()
        {
            RemainingToExecute = _executeRate;
        }

        public void ResetRemainingDuration()
        {
            RemainingDuration = _duration;
        }
        public bool IsExecuteTime() => Mathf.Approximately(RemainingToExecute, 0f);
        public bool IsDurationEnd() => Mathf.Approximately(RemainingDuration, 0f);
    }
}
