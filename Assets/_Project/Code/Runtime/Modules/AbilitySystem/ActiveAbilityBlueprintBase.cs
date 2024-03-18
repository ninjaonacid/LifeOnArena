using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Modules.AbilitySystem.GameplayEffects;
using Code.Runtime.Services.BattleService;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
    public abstract class ActiveAbilityBlueprintBase : AbilityBlueprintBase
    {
        public float Cooldown;
        public float CurrentCooldown;
        public float ActiveTime;
        public float CurrentActiveTime;
        public int Price;
        public bool IsCastAbility; 
        public VisualEffectData VisualEffectData;
        
        [SerializeField] private List<GameplayEffectBlueprint> _statusTemplates;
        protected IReadOnlyList<GameplayEffect> StatusEffects => _statusTemplates.Select(x => x.GetGameplayEffect()).ToList();

        protected VisualEffectFactory _visualEffectFactory;
        protected ProjectileFactory _projectileFactory;
        protected BattleService _battleService;
        public abstract ActiveAbility GetAbility();

        public void InitServices(
            VisualEffectFactory visualEffectFactory,
            ProjectileFactory projectileFactory,
            BattleService battleService)
        {
            _visualEffectFactory = visualEffectFactory;
            _projectileFactory = projectileFactory;
            _battleService = battleService;
        }
        
    }
}
