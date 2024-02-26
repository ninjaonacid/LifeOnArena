using System;
using System.Collections.Generic;
using Code.ConfigData.StatSystem;
using Code.ConfigData.StatusEffects;
using Code.Entity.StatusEffects;
using UnityEngine;
using Attribute = Code.ConfigData.StatSystem.Attribute;

namespace Code.Entity
{
    public class StatusEffectController : MonoBehaviour
    {
        [SerializeField] private StatController _statController;

        private readonly List<DurationalGameplayEffect> _activeEffects = new List<DurationalGameplayEffect>();

        private void Update()
        {
            HandleDuration();
        }

        public void ApplyEffectToSelf(GameplayEffect effect)
        {
            if (effect.Type == EffectDurationType.Instant)
            {
                ExecuteEffect(effect);
            }

            if (effect is DurationalGameplayEffect durationalEffect)
            {
                if (_activeEffects.Contains(durationalEffect)) return;
                _activeEffects.Add(durationalEffect);
            }
            
        }
        
        public bool IsEntityDisabled()
        {
            foreach (var status in _activeEffects)
            {
                if (status.IsDisablingEffect) return true;
            }

            return false;
        }

        private void HandleDuration()
        {
            foreach (var activeEffect in _activeEffects)
            {
                activeEffect.TickEffect(Time.deltaTime);

                if (activeEffect.IsExecuteTime())
                {
                    ExecuteEffect(activeEffect);
                    
                    activeEffect.ResetExecutionTime();
                }

                if (activeEffect.IsDurationEnd())
                {
                    RemoveEffect(activeEffect);
                    
                    activeEffect.ResetRemainingDuration();
                }
            }
        }

        private void RemoveEffect(DurationalGameplayEffect effect)
        {
            if (_activeEffects.Contains(effect))
            {
                _activeEffects.Remove(effect);
            }
        }

        private void ExecuteEffect(GameplayEffect effect)
        {
            for (var i = 0; i < effect.ModifierTemplates.Count; i++)
            {
                var modifierTemplate = effect.ModifierTemplates[i];
                if (_statController.Stats.TryGetValue(modifierTemplate.StatName, out var stat))
                {
                    if (stat is Attribute attribute)
                    {
                        attribute.ApplyModifier(effect.Modifiers[i]);
                    }
                }
            }
        }
    }
}