using System;
using System.Linq;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.Hero;
using Code.Runtime.UI.View.HUD.Skills;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AbilityController : MonoBehaviour
    {
        public event Action OnAbilityUse;
        public event Action OnSkillChanged;
        
        [SerializeField] protected AbilityCooldownController _cooldownController;
        [SerializeField] protected AbilitySlot[] _abilitySlots;
        
        public AbilitySlot[] AbilitySlots => _abilitySlots;
        public ActiveAbility ActiveAbility => _activeAbility;

        private ActiveAbility _activeAbility;

        protected IAbilityFactory _abilityFactory;

        [Serializable]
        public class AbilitySlot
        {
            public AbilityIdentifier AbilityIdentifier;
            public ActiveAbility Ability;
            public AbilitySlotID AbilitySlotID;
        }
        
        [Inject]
        public void Construct(IAbilityFactory abilityFactory)
        {
            _abilityFactory = abilityFactory;
        }

        public virtual void Start()
        {
            foreach (var slot in _abilitySlots)
            {
                if (slot.AbilityIdentifier is not null)
                {
                    slot.Ability =
                        _abilityFactory.CreateActiveAbility(slot.AbilityIdentifier.Id);
                }
            }
        }

        public bool TryActivateAbility(AbilityIdentifier abilityId)
        {
            AbilitySlot slot = _abilitySlots.FirstOrDefault(x => x.AbilityIdentifier == abilityId);

            if (slot != null)
            {
                if (CanActivateAbility(slot.Ability))
                {
                    _activeAbility = slot.Ability;
                    slot.Ability.Use(gameObject, null);
                    slot.Ability.State = AbilityState.Active;
                    _cooldownController.StartCooldown(slot.Ability);
                    OnAbilityUse?.Invoke();
                }
            }

            return false;
        }

        public bool TryActivateAbility(ActiveAbility ability)
        {
            AbilitySlot slot = _abilitySlots.FirstOrDefault(x => x.Ability == ability);

            if (slot != null)
            {
                if (CanActivateAbility(slot.Ability))
                {
                    _activeAbility = slot.Ability;
                    slot.Ability.Use(gameObject, null);
                    slot.Ability.State = AbilityState.Active;
                    _cooldownController.StartCooldown(slot.Ability);
                    OnAbilityUse?.Invoke();
                }
            }

            return false;
        }

        public bool TryActivateAbility(string abilityName)
        {
            AbilitySlot slot = _abilitySlots.FirstOrDefault(x => x.AbilityIdentifier.name == abilityName);
            
            if (slot != null)
            {
                if (CanActivateAbility(slot.Ability))
                {
                    _activeAbility = slot.Ability;
                    slot.Ability.Use(gameObject, null);
                    slot.Ability.State = AbilityState.Active;
                    _cooldownController.StartCooldown(slot.Ability);
                    OnAbilityUse?.Invoke();
                }
            }

            return false;
        }

        private bool CanActivateAbility(ActiveAbility ability)
        {
            if (ability.State == AbilityState.Ready)
            {
                if (_abilitySlots.Any(x => x.Ability.IsActive()))
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        protected void OnAbilityChanged()
        {
            OnSkillChanged?.Invoke();
        }
    }
}
