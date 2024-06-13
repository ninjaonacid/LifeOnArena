using System;
using System.Collections.Generic;
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
        private readonly Queue<ActiveAbility> _abilityQueue = new();
        private readonly int _abilityQueueLimit = 2;

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
                        _abilityFactory.CreateActiveAbility(slot.AbilityIdentifier.Id, this);
                }
            }
        }

        private void Update()
        {
            HandleAbilityQueue();
        }

        private void HandleAbilityQueue()
        {
            if (_activeAbility != null && !_activeAbility.IsActive())
            {
                if (_abilityQueue.Count > 0)
                {
                    var nextAbility = _abilityQueue.Dequeue();
                    ActivateAbility(nextAbility);
                }
            }
        }
        

        public bool CanActivateAbility(AbilityIdentifier ability)
        {
            AbilitySlot slot = _abilitySlots.FirstOrDefault(x => x.AbilityIdentifier == ability);
            
            if (slot != null && slot.Ability.State == AbilityState.Ready)
            {
                if (_abilitySlots != null && _abilitySlots.Any(x => x.Ability != null && x.Ability.IsActive()))
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        private bool CanActivateAbility(ActiveAbility ability)
        {
            if (ability.State == AbilityState.Ready)
            {
                if (_abilitySlots != null && _abilitySlots.Any(x => x.Ability != null && x.Ability.IsActive()))
                {
                    if(_abilityQueue.Count < _abilityQueueLimit)
                        _abilityQueue.Enqueue(ability);
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
        
        public bool TryActivateAbility(AbilityIdentifier abilityId)
        {
            return TryActivateAbility(slot => slot.AbilityIdentifier == abilityId);
        }

        public bool TryActivateAbility(ActiveAbility ability)
        {
            return TryActivateAbility(slot => slot.Ability == ability);
        }

        public bool TryActivateAbility(string abilityName)
        {
            return TryActivateAbility(slot => slot.AbilityIdentifier.name == abilityName);
        }

        private bool TryActivateAbility(Func<AbilitySlot, bool> predicate)
        {
            AbilitySlot slot = _abilitySlots.FirstOrDefault(predicate);

            if (slot != null && CanActivateAbility(slot.Ability))
            {
                ActivateAbility(slot.Ability);
                return true;
            }

            return false;
        }

        private void ActivateAbility(ActiveAbility ability)
        {
            ability.Use(this, null);
            ability.State = AbilityState.Active;
            _activeAbility = ability;
            _cooldownController.StartCooldown(ability);
            OnAbilityUse?.Invoke();
        }
    }
}
