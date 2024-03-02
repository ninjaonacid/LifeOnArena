﻿using System.Collections.Generic;
using Code.Runtime.ConfigData;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;
using VContainer;
using Attribute = Code.Runtime.Modules.StatSystem.Attribute;

namespace Code.Runtime.Entity
{
    public class StatusEffectController : MonoBehaviour
    {
        [SerializeField] private TagController _tagController;
        [SerializeField] private StatController _statController;
        [SerializeField] private EntityHurtBox _hurtBox;
        
        
        private VisualEffectFactory _visualFactory;

        private readonly List<DurationalGameplayEffect> _activeEffects = new List<DurationalGameplayEffect>();
        private List<DurationalGameplayEffect> _effectsToRemove = new List<DurationalGameplayEffect>();
        
        [Inject]
        public void Construct(VisualEffectFactory visualFactory)
        {
            _visualFactory = visualFactory;
        }
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
                PlayVisualEffect(effect);
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

                if (activeEffect.IsDurationEnd())
                {
                    _effectsToRemove.Add(activeEffect);
                    
                    activeEffect.ResetRemainingDuration();
                }
            }

            foreach (var effect in _effectsToRemove)
            {
                RemoveEffect(effect);
            }
            
            _effectsToRemove.Clear();
        }

        private void RemoveEffect(DurationalGameplayEffect effect)
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

        private async void PlayVisualEffect(GameplayEffect effect)
        {
            var visualEffect = await _visualFactory.CreateVisualEffect(effect.StatusVisualEffect.VisualEffectData.Identifier.Id);
            var goCenter = _hurtBox.GetCenterTransform();
            var goPosition = transform.position;
            
            if (effect.StatusVisualEffect)
            {
                if (effect.StatusVisualEffect.PlayLocation == PlayLocation.Above)
                {
                    var height = _hurtBox.GetHeight();

                    visualEffect.transform.position = new Vector3(goPosition.x, (height.y * Vector3.up).y, goPosition.z);
                }
                
                else if(effect.StatusVisualEffect.PlayLocation == PlayLocation.Below)
                {
                    var height = _hurtBox.GetHeight();

                    visualEffect.transform.position = new Vector3(goPosition.x, (-height.y), goPosition.z);
                }
                
                else if (effect.StatusVisualEffect.PlayLocation == PlayLocation.Center)
                {
                    var center = _hurtBox.GetCenterTransform();

                    visualEffect.transform.position = center;
                }
            }
        }
    }
}