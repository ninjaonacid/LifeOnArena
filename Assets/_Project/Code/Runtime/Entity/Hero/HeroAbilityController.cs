using System;
using System.Collections.Generic;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class HeroAbilityController : AbilityController, ISaveLoader
    {
        private PlayerControls _controls;

        private readonly List<InputAction> _skillActions = new();
        private readonly List<Action<InputAction.CallbackContext>> _hashedDelegates = new();

        [Inject]
        public void Construct(PlayerControls controls)
        {
            _controls = controls;
        }

        public override void Start()
        {
            base.Start();
            
            _skillActions.Add(_controls.Player.SkillSlot1);
            _skillActions.Add(_controls.Player.SkillSlot2);
            _skillActions.Add(_controls.Player.Attack);

            for (var index = 0; index < _skillActions.Count; index++)
            {
                var index1 = index;

                Action<InputAction.CallbackContext> delegateSubscribe =
                    delegate(InputAction.CallbackContext context) { OnSkillSlot(context, index1); };

                _hashedDelegates.Add(delegateSubscribe);

                _skillActions[index].performed += delegateSubscribe;
            }
        }

        private void OnDestroy()
        {
            for (int index = 0; index < _skillActions.Count; index++)
            {
                var index1 = index;
                _skillActions[index].performed -= _hashedDelegates[index1];
            }
        }

        private void OnSkillSlot(InputAction.CallbackContext ctx, int index)
        {
            var abilityToUse = _abilitySlots[index].Ability;
            if (abilityToUse == null) return;

            TryActivateAbility(_abilitySlots[index].Ability);
        }

        public void LoadData(PlayerData data)
        {
            var skillsData = data.AbilityData;

            if (skillsData.EquippedAbilities.Count > 0)
            {
                for (var index = 0; index < skillsData.EquippedAbilities.Count; index++)
                {
                    var slot = _abilitySlots[index];

                    slot.Ability =
                        _abilityFactory.CreateActiveAbility(skillsData.EquippedAbilities[index].AbilityId, this);
                }
            }

            OnAbilityChanged();
        }
    }
}