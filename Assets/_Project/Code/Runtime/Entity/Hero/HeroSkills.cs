using System;
using System.Collections.Generic;
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
    public class HeroSkills : MonoBehaviour, ISaveLoader
    {
        public event Action OnSkillChanged;
        public event Action OnAbilityUse;

        [SerializeField] private HeroAbilityCooldown _heroCooldown;
        [SerializeField] private SkillSlot[] _skillSlots;
        public SkillSlot[] SkillSlots => _skillSlots;
        public AbilityTemplateBase ActiveSkill => _activeSkill;
        
        private AbilityTemplateBase _activeSkill;
        private AbilityData _abilityData;
        private IAbilityFactory _abilityFactory;
        private PlayerControls _controls;

        private readonly List<InputAction> _skillActions = new();
        private readonly List<Action<InputAction.CallbackContext>> _hashedDelegates = new();

        [Serializable]
        public class SkillSlot
        {
            public AbilityTemplateBase AbilityTemplate;
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
               if (slot.AbilityTemplate)
               {
                   slot.AbilityTemplate = _abilityFactory.InitializeAbilityTemplate(slot.AbilityTemplate);
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
            var abilityTemplate = _skillSlots[index].AbilityTemplate;
            if (abilityTemplate == null) return;
            if (abilityTemplate.State is AbilityState.Cooldown or AbilityState.Active) return;
            
            _skillSlots[index].AbilityTemplate.GetAbility().Use(this.gameObject, null);
            _skillSlots[index].AbilityTemplate.State = AbilityState.Active;
            _activeSkill = _skillSlots[index].AbilityTemplate;
            _heroCooldown.StartCooldown(_skillSlots[index].AbilityTemplate);
            OnAbilityUse?.Invoke();
        }
        
        public void LoadData(PlayerData data)
        {
            var skillsData = data.AbilityData;

            if (skillsData.EquippedSlots.Count > 0)
            {
                for (var index = 0; index < skillsData.EquippedSlots.Count; index++)
                {
                    var slot = _skillSlots[index];

                    slot.AbilityTemplate = _abilityFactory.CreateAbilityTemplate(skillsData.EquippedSlots[index].AbilityId);

                }
            }
            
            OnSkillChanged?.Invoke();
        }
    }
}
