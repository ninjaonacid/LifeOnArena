using System;
using Code.ConfigData.Ability;
using Code.Core.Factory;
using Code.Data.PlayerData;
using Code.Logic.WaveLogic;
using Code.Services.PersistentProgress;
using Code.UI.View.HUD.Skills;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Entity.Hero
{
    public class HeroSkills : MonoBehaviour, ISaveLoader
    {
        public event Action OnSkillChanged;

        [SerializeField] private HeroAbilityCooldown _heroCooldown;

        public SkillSlot[] SkillSlots => _skillSlots;
        
        [SerializeField] private SkillSlot[] _skillSlots;
        public AbilityTemplateBase ActiveSkill => _activeSkill;
        
        private AbilityTemplateBase _activeSkill;
        private AbilityData _abilityData;
        private IAbilityFactory _abilityFactory;
        private PlayerControls _controls;
        private EnemySpawnerController _controller;

        [Serializable]
        public class SkillSlot
        {
            public AbilityTemplateBase AbilityTemplate;
            public AbilitySlotID AbilitySlotID;
        }

        [Inject]
        public void Construct(IAbilityFactory abilityFactory, PlayerControls controls)
        {
            _abilityFactory = abilityFactory;
            _controls = controls;
        }
        private void Start()
        {
            _controls.Player.SkillSlot1.performed += OnSkillSlot1;
            _controls.Player.SkillSlot2.performed += OnSkillSlot2;

            foreach (var slot in _skillSlots)
            {
                if (slot.AbilityTemplate)
                {
                    slot.AbilityTemplate = _abilityFactory.InitializeAbilityTemplate(slot.AbilityTemplate);
                }
            }
        }

        private void OnDestroy()
        {
            _controls.Player.SkillSlot1.performed -= OnSkillSlot1;
            _controls.Player.SkillSlot2.performed -= OnSkillSlot2;
        }

        private void OnSkillSlot1(InputAction.CallbackContext context)
        {
            var abilityTemplate = _skillSlots[0].AbilityTemplate;
            if (abilityTemplate == null) return;
            if (abilityTemplate.State is AbilityState.Cooldown or AbilityState.Active) return;
            
            _skillSlots[0].AbilityTemplate.GetAbility().Use(this.gameObject, null);
            _skillSlots[0].AbilityTemplate.State = AbilityState.Active;
            _activeSkill = _skillSlots[0].AbilityTemplate;
            _heroCooldown.StartCooldown(_skillSlots[0].AbilityTemplate);
        }

        private void OnSkillSlot2(InputAction.CallbackContext context)
        {
            var abilityTemplate = _skillSlots[1].AbilityTemplate;
            if (abilityTemplate == null) return;
            if (abilityTemplate.State is AbilityState.Cooldown or AbilityState.Active) return;
            
            _skillSlots[1].AbilityTemplate.GetAbility().Use(this.gameObject, null);
            _skillSlots[1].AbilityTemplate.State = AbilityState.Active;
            _activeSkill = _skillSlots[1].AbilityTemplate;
            _heroCooldown.StartCooldown(_skillSlots[1].AbilityTemplate);
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
