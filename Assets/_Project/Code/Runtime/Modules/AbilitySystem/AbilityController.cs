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
        public event Action<ActiveAbility> OnAbilityUse;
        public event Action OnSkillChanged;

        [SerializeField] protected AbilityCooldownController _cooldownController;
        [SerializeField] protected AbilitySlot[] _abilitySlots;
        public AbilitySlot[] AbilitySlots => _abilitySlots;
        public ActiveAbility ActiveAbility => _activeAbility;

        private AbilityQueue _abilityQueue;
        private readonly int _abilityQueueLimit = 1;
        private readonly float _queueTimeWindow = 1;
        private ActiveAbility _activeAbility;

        protected AbilityFactory _abilityFactory;

        [Serializable]
        public class AbilitySlot
        {
            public AbilityIdentifier AbilityIdentifier;
            public ActiveAbility Ability;
            public AbilitySlotID AbilitySlotID;
        }

        [Inject]
        public void Construct(AbilityFactory abilityFactory)
        {
            _abilityFactory = abilityFactory;
        }

        public virtual void Start()
        {
            _abilityQueue = new AbilityQueue(_abilityQueueLimit, _queueTimeWindow);

            foreach (var slot in _abilitySlots)
            {
                if (slot.AbilityIdentifier != null)
                {
                    slot.Ability = _abilityFactory.CreateActiveAbility(slot.AbilityIdentifier.Id, this);
                }
            }

            OnSkillChanged?.Invoke();
        }

        private void Update()
        {
            HandleAbilityActivation();
        }

        private void HandleAbilityActivation()
        {
            if (_activeAbility != null)
            {
                if (_activeAbility.State == AbilityState.Active)
                {
                    if (_activeAbility.CanCombo && _abilityQueue.HasNext())
                    {
                        var nextAbility = _abilityQueue.Peek();
                        if (nextAbility.CanCombo && !_cooldownController.IsOnCooldown(nextAbility))
                        {
                            _abilityQueue.Dequeue();
                            ActivateAbility(nextAbility);
                        }
                    }
                    return;
                }

                if (_activeAbility.State == AbilityState.Active)
                {
                    return;
                }

                _activeAbility = null;
            }

            if (_abilityQueue.HasNext())
            {
                var nextAbility = _abilityQueue.Peek();
                if (CanActivateAbility(nextAbility))
                {
                    _abilityQueue.Dequeue();
                    ActivateAbility(nextAbility);
                }
            }
        }

        protected void OnAbilityChanged()
        {
            OnSkillChanged?.Invoke();
        }

        public bool TryActivateAbility(AbilityIdentifier abilityId)
        {
            AbilitySlot slot = _abilitySlots.FirstOrDefault(x => x.AbilityIdentifier == abilityId);
            if (slot?.Ability == null)
            {
                return false;
            }

            if (slot.Ability.State == AbilityState.Ready || slot.Ability.CanCombo)
            {
                if (_activeAbility == null || !_activeAbility.IsActive())
                {
                    if (CanActivateAbility(slot.Ability))
                    {
                        ActivateAbility(slot.Ability);
                        return true;
                    }
                }
                
                return _abilityQueue.TryEnqueue(slot.Ability);
            }

            return false;
        }

        private bool CanActivateAbility(ActiveAbility ability)
        {
            return ability.State == AbilityState.Ready && !_cooldownController.IsOnCooldown(ability);
        }

        private void ActivateAbility(ActiveAbility ability)
        {
            ability.State = AbilityState.Active;
            _activeAbility = ability;
            ability.Use(this, null);
            _cooldownController.StartCooldown(ability);
            OnAbilityUse?.Invoke(ability);
        }
    }
}