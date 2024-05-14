using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Factory;
using Code.Runtime.Services.BattleService;

namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class ActiveAbility : Ability
    {
        public float Cooldown { get; }
        public float CurrentCooldown { get; set; }
        public float ActiveTime { get; }
        public float CurrentActiveTime { get; set; }
        public bool IsCastAbility { get; }
        public AbilityState State { get; set; }
        
        protected VisualEffectFactory _visualEffectFactory;
        protected ProjectileFactory _projectileFactory;
        protected BattleService _battleService;
        protected AudioService _audioService;

        public bool IsActive() => State is AbilityState.Active;

        public bool IsOnCooldown() => State is AbilityState.Cooldown;

        public bool IsReady() => State is AbilityState.Ready;

        protected ActiveAbility(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
            Cooldown = abilityBlueprint.Cooldown;
            CurrentCooldown = abilityBlueprint.Cooldown;
            ActiveTime = abilityBlueprint.ActiveTime;
            CurrentActiveTime = abilityBlueprint.ActiveTime;
            IsCastAbility = abilityBlueprint.IsCastAbility;
            
            State = AbilityState.Ready;
        }

        public void InjectServices(
            VisualEffectFactory visualEffectFactory,
            ProjectileFactory projectileFactory,
            BattleService battleService,
            AudioService audioService)
        {
            _visualEffectFactory = visualEffectFactory;
            _projectileFactory = projectileFactory;
            _battleService = battleService;
            _audioService = audioService;
        }
    }
}