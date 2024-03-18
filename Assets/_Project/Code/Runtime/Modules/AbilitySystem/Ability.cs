using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class Ability
    {
        public float Cooldown { get; } 
        public float CurrentCooldown { get; set; }
        public float ActiveTime { get; }
        public float CurrentActiveTime { get; set; }
        public AbilityState AbilityState { get; }
        public bool IsCastAbility { get; }
        
        protected Ability(float cooldown, float activeTime, bool isCastAbility)
        {
            Cooldown = cooldown;
            ActiveTime = activeTime;
            IsCastAbility = isCastAbility;

            AbilityState = AbilityState.Ready;
            CurrentCooldown = cooldown;
            CurrentActiveTime = activeTime;
        }

        
        public abstract void Use(GameObject caster, GameObject target);

        public virtual bool IsActive() =>  AbilityState == AbilityState.Active;
        public virtual bool IsReady() => AbilityState == AbilityState.Ready;


    }
}