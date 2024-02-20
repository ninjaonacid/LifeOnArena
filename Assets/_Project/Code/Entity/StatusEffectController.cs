using System.Collections.Generic;
using Code.ConfigData.StatSystem;
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
        
        private List<StatusEffect> _statusEffects;

        public void ApplyEffectToSelf(StatusEffect effect)
        {
            if (_statusEffects.Contains(effect))
            {
                return;
            }

            for (var i = 0; i < effect.ModifierTemplates.Count; i++)
            {
                var modifierTemplate = effect.ModifierTemplates[i];
                if (_statController.Stats.TryGetValue(modifierTemplate.StatName, out var stat))
                {
                    stat.AddModifier(effect.Modifiers[i]);
                }
            }

            if (effect is DurationalStatusEffect)
            {
                
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