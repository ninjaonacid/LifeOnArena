using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class ActiveAbilityBlueprintBase : AbilityBlueprintBase
    {
        public float Cooldown;
        public float ActiveTime;
        public bool IsCastAbility; 
        public VisualEffectData VisualEffectData;
        
        [SerializeField] private List<GameplayEffectBlueprint> _gameplayEffectBlueprints;
        public IReadOnlyList<GameplayEffect> GameplayEffects => 
            _gameplayEffectBlueprints.Select(x => x.GetGameplayEffect()).ToList();

        public abstract ActiveAbility GetAbility();
    }
}
