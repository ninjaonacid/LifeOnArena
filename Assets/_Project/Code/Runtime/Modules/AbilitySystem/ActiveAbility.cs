using System.Collections.Generic;
using Code.Runtime.Entity.StatusEffects;

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

        protected ActiveAbility(IReadOnlyList<GameplayEffect> effects, float cooldown, float activeTime, bool isCastAbility) : base(effects)
        {
            Cooldown = cooldown;
            CurrentCooldown = cooldown;
            ActiveTime = activeTime;
            CurrentActiveTime = activeTime;
            IsCastAbility = isCastAbility;

            State = AbilityState.Ready;
        }

        public bool IsActive() => State is AbilityState.Active;
        public bool IsOnCooldown() => State is AbilityState.Cooldown;
    }
}