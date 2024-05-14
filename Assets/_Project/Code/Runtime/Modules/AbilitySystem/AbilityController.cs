using System;
using System.Linq;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.Hero;
using Code.Runtime.UI.View.HUD.Skills;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AbilityController : MonoBehaviour
    {
        public event Action OnAbilityUse;
        public event Action OnSkillChanged;
        
        [SerializeField] protected AbilityCooldownController _сooldownController;
        [SerializeField] protected AbilitySlot[] _skillSlots;
        
        public AbilitySlot[] SkillSlots => _skillSlots;
        public ActiveAbility ActiveAbility => _activeAbility;

        private ActiveAbility _activeAbility;
        
        private IAbilityFactory _abilityFactory;

        [Serializable]
        public class AbilitySlot
        {
            public AbilityIdentifier AbilityIdentifier;
            public ActiveAbility Ability;
            public AbilitySlotID AbilitySlotID;
        }

        public bool TryActivateAbility(AbilityIdentifier abilityId)
        {
            AbilitySlot slot = _skillSlots.FirstOrDefault(x => x.AbilityIdentifier == abilityId);

            if (slot != null)
            {
                if (CanActivateAbility(slot.Ability))
                {
                    _activeAbility = slot.Ability;
                    slot.Ability.Use(gameObject, null);
                    _сooldownController.StartCooldown(slot.Ability);
                    OnAbilityUse?.Invoke();
                }
            }

            return false;
        }

        public bool CanActivateAbility(ActiveAbility ability)
        {
            if (ability.State == AbilityState.Ready)
            {
                return true;
            }

            return false;
        }
        
        
    }
}
