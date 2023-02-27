
using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.StaticData.Ability
{
    public enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    public abstract class AbilityTemplateBase : ScriptableObject
    {
        [ScriptableObjectId]
        public string Id;
        public string StateMachineId;
        public float Cooldown;
        public float CurrentCooldown;
        public float ActiveTime;
        public float CurrentActiveTime;
        public Sprite Icon;
        public AbilityState State;
        public AbilitySlotID AbilitySlot;
        public abstract IAbility GetAbility();

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
