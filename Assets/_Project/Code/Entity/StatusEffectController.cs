using System.Collections.Generic;
using Code.ConfigData.StatSystem;
using Code.ConfigData.StatusEffects;
using Code.Entity.StatusEffects;
using UnityEngine;

namespace Code.Entity
{
    public class StatusEffectController : MonoBehaviour
    {
        [SerializeField] private StatController _statController;
        private bool _isDisabled;
        public bool IsDisabled => _isDisabled;
        
        private float _disableDuration;
        public float DisableDuration => _disableDuration;

        private readonly List<DurationalStatusEffect> _activeEffects = new List<DurationalStatusEffect>();

        public void ApplyEffectToSelf(StatusEffect effect)
        {
            if (effect.Type == EffectDurationType.Instant)
            {
                ExecuteEffect(effect);
            }

            if (effect is DurationalStatusEffect durationalEffect)
            {
                if (_activeEffects.Contains(durationalEffect)) return;
                _activeEffects.Add(durationalEffect);
            }
            
        }
        
        public bool IsEntityDisabled()
        {
            foreach (var status in _activeEffects)
            {
                if (status is StunStatusEffect)
                {
                    return true;
                }
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

        private void RemoveEffect(DurationalStatusEffect effect)
        {
            if (_activeEffects.Contains(effect))
            {
                _activeEffects.Remove(effect);
            }
        }

        private void ExecuteEffect(StatusEffect effect)
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

        private void Update()
        {
            _disableDuration -= Time.deltaTime;

            if (_disableDuration <= 0)
            {
                _isDisabled = false;
            }
        }

       
    }
}