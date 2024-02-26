using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData.StatusEffects;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Services.BattleService;
using UnityEngine;

namespace Code.Runtime.ConfigData.Ability
{
    public enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
    public abstract class AbilityTemplateBase : AbilityBase
    {
        public float Cooldown;
        public float CurrentCooldown;
        public float ActiveTime;
        public float CurrentActiveTime;
        public int Price;
        public AbilityState State;
        public bool IsCastAbility; 
        public VfxData VfxData;
        
        [SerializeField] private List<GameplayEffectBlueprint> _statusTemplates;

        protected IReadOnlyList<GameplayEffect> StatusEffects => _statusTemplates.Select(x => x.GetGameplayEffect()).ToList();

        protected ParticleFactory _particleFactory;
        protected BattleService _battleService;
        public abstract IAbility GetAbility();

        public void InitServices(
            ParticleFactory particleFactory,
            BattleService battleService)
        {
            _particleFactory = particleFactory;
            _battleService = battleService;
        }
        
        public virtual bool IsReady() =>
            State == AbilityState.Ready;
        
        public virtual bool IsActive() =>
            State == AbilityState.Active;

        private void OnEnable()
        {
            State = AbilityState.Ready;
        }
    }
}
