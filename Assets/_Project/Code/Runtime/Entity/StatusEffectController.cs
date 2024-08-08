using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.Runtime;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Attribute = Code.Runtime.Modules.StatSystem.Runtime.Attribute;

namespace Code.Runtime.Entity
{
    public class StatusEffectController : MonoBehaviour
    {
        [SerializeField] private TagController _tagController;
        [SerializeField] private StatController _statController;
        [SerializeField] private VisualEffectController _vfxController;
        
        private readonly List<DurationalGameplayEffect> _activeEffects = new List<DurationalGameplayEffect>();
        private readonly List<DurationalGameplayEffect> _effectsToRemove = new List<DurationalGameplayEffect>();
        
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

            if (effect.EffectTags.Count >= 1)
            {
                foreach (var gameplayTag in effect.EffectTags)
                {
                    _tagController.AddTag(gameplayTag);
                }
            }

            if (effect.StatusVisualEffect != null)
            {
                PlayVisualEffect(effect).Forget();
            }
        }

        public void AddEffectToRemove(DurationalGameplayEffect effect)
        {
            if (_activeEffects.Contains(effect) && !_effectsToRemove.Contains(effect))
            {
                _effectsToRemove.Add(effect);
            }
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

                if (activeEffect.IsDurationEnd() && !_effectsToRemove.Contains(activeEffect))
                {
                    _effectsToRemove.Add(activeEffect);

                    activeEffect.ResetRemainingDuration();
                }
            }

            foreach (var effect in _effectsToRemove)
            {
                RemoveEffectInternal(effect);
            }

            _effectsToRemove.Clear();
        }

        private void RemoveEffectInternal(DurationalGameplayEffect effect)
        {
            if (_activeEffects.Contains(effect))
            {
                foreach (var gameplayTag in effect.EffectTags)
                {
                    _tagController.RemoveTag(gameplayTag);
                }

                _activeEffects.Remove(effect);
            }
        }

        private void ExecuteEffect(GameplayEffect effect)
        {
            for (var i = 0; i < effect.ModifierBlueprints.Count; i++)
            {
                var modifierTemplate = effect.ModifierBlueprints[i];
                if (_statController.Stats.TryGetValue(modifierTemplate.StatName, out var stat))
                {
                    if (stat is Attribute attribute)
                    {
                        attribute.ApplyModifier(effect.Modifiers[i]);
                    }
                }
            }
        }

        private async UniTask PlayVisualEffect(GameplayEffect effect)
        {
            await _vfxController.PlayVisualEffect(effect.StatusVisualEffect);
        }
    }
}