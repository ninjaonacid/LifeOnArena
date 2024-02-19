using System;
using System.Collections.Generic;
using Code.Entity.StatusEffects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Entity
{
    public class StatusEffectController : MonoBehaviour
    {
        private bool _isDisabled;
        public bool IsDisabled => _isDisabled;
        
        private float _disableDuration;
        public float DisableDuration => _disableDuration;
        
        private List<StatusEffect> _statusEffects;

        public void ApplyEffectToSelf(StatusEffect effect)
        {
            if (_statusEffects.Contains(effect))
            {
                return;
            }
            
            _statusEffects.Add(effect);
            
        }

        private void Update()
        {
            _disableDuration -= Time.deltaTime;

            if (_disableDuration <= 0)
            {
                _isDisabled = false;
            }
        }

        public void RemoveEffect(StatusEffect effect)
        {
            if (_statusEffects.Contains(effect))
            {
                _statusEffects.Remove(effect);
            }
        }
    }
}