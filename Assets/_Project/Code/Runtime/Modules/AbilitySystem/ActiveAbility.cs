using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Entity.StatusEffects;
using UnityEngine;

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
        

        public bool IsActive() => State is AbilityState.Active;
        public bool IsOnCooldown() => State is AbilityState.Cooldown;

        protected ActiveAbility(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
            Cooldown = abilityBlueprint.Cooldown;
            CurrentCooldown = abilityBlueprint.CurrentCooldown;
            ActiveTime = abilityBlueprint.ActiveTime;
            CurrentActiveTime = abilityBlueprint.ActiveTime;
            IsCastAbility = abilityBlueprint.IsCastAbility;
            
            State = AbilityState.Ready;
        }
    }
}