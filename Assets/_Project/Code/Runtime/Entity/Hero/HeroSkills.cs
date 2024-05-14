using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.View.HUD.Skills;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class HeroSkills : AbilityController, ISaveLoader
    {
        public event Action OnAbilityUse;
        public event Action OnSkillChanged;
        
        [SerializeField] private AbilityCooldownController CooldownController;
        [SerializeField] private AbilitySlot[] _skillSlots;
        public AbilitySlot[] SkillSlots => _skillSlots;
        public ActiveAbility ActiveAbility => _activeAbility;

        private ActiveAbility _activeAbility;
        private IAbilityFactory _abilityFactory;
        private PlayerControls _controls;

        private readonly List<InputAction> _skillActions = new();
        private readonly List<Action<InputAction.CallbackContext>> _hashedDelegates = new();

        [Serializable]
        public class AbilitySlot
        {
            public AbilityIdentifier AbilityIdentifier;
            public ActiveAbility Ability;
            public AbilitySlotID AbilitySlotID;
        }

        [Inject]
        public void Construct(IAbilityFactory abilityFactory, PlayerControls controls, SceneLoader sceneLoader)
        {
            _abilityFactory = abilityFactory;
            _controls = controls;

            _controls.Player.RestartScene.performed += (ctx) => sceneLoader.Load("StoneDungeon_Arena_1");
        }

        private void Start()
        {
            _skillActions.Add(_controls.Player.SkillSlot1);
            _skillActions.Add(_controls.Player.SkillSlot2);

            foreach (var slot in _skillSlots)
            {
                if (slot.AbilityIdentifier is not null)
                {
                    slot.Ability =
                        _abilityFactory.CreateActiveAbility(slot.AbilityIdentifier.Id);
                }
            }

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
            var abilityToUse = _skillSlots[index].Ability;
            if (abilityToUse == null) return;
            if (abilityToUse.State is AbilityState.Cooldown or AbilityState.Active) return;
            if (_skillSlots.Any(slot => slot.Ability != null && slot.Ability.IsActive()))
            {
                return;
            }

            _skillSlots[index].Ability.Use(this.gameObject, null);
            _skillSlots[index].Ability.State = AbilityState.Active;
            _activeAbility = _skillSlots[index].Ability;
            CooldownController.StartCooldown(_skillSlots[index].Ability);
            OnAbilityUse?.Invoke();
        }

        public void LoadData(PlayerData data)
        {
            var skillsData = data.AbilityData;

            if (skillsData.EquippedAbilities.Count > 0)
            {
                for (var index = 0; index < skillsData.EquippedAbilities.Count; index++)
                {
                    var slot = _skillSlots[index];

                    slot.Ability =
                        _abilityFactory.CreateActiveAbility(skillsData.EquippedAbilities[index].AbilityId);
                }
            }

            OnSkillChanged?.Invoke();
        }
    }
}