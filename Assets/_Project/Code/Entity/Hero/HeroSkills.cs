using System;
using Code.ConfigData.Ability;
using Code.Core.Factory;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.UI.HUD.Skills;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Entity.Hero
{
    public class HeroSkills : MonoBehaviour, ISaveReader
    {
        public event Action OnSkillChanged;

        [SerializeField] private HeroAbilityCooldown _heroCooldown;
      
        public SkillSlot[] SkillSlots;
        public AbilityTemplateBase ActiveSkill => _activeSkill;
        
        private AbilityTemplateBase _activeSkill;
        private SkillSlotsData _skillSlotsData;
        private IAbilityFactory _abilityFactory;
        private PlayerControls _controls;


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
        }

        private void OnDestroy()
        {
            _controls.Player.SkillSlot1.performed -= OnSkillSlot1;
            _controls.Player.SkillSlot2.performed -= OnSkillSlot2;
        }

        private void OnSkillSlot1(InputAction.CallbackContext context)
        {
            if (SkillSlots[0].AbilityTemplate == null) return;

            SkillSlots[0].AbilityTemplate.GetAbility().Use(this.gameObject, null);
            SkillSlots[0].AbilityTemplate.State = AbilityState.Active;
            _activeSkill = SkillSlots[0].AbilityTemplate;
            _heroCooldown.StartCooldown(SkillSlots[0].AbilityTemplate);
        }

        private void OnSkillSlot2(InputAction.CallbackContext context)
        {
            if (SkillSlots[1].AbilityTemplate == null) return;
            
            SkillSlots[1].AbilityTemplate.GetAbility().Use(this.gameObject, null);
            SkillSlots[1].AbilityTemplate.State = AbilityState.Active;
            _activeSkill = SkillSlots[1].AbilityTemplate;
            _heroCooldown.StartCooldown(SkillSlots[1].AbilityTemplate);
        }


        public void LoadData(PlayerData data)
        {
            var skillsData = data.SkillSlotsData;

            if (skillsData.SkillIds.Count > 0)
            {
                for (var index = 0; index <= skillsData.SkillIds.Count; index++)
                {
                    var slot = SkillSlots[index];

                    slot.AbilityTemplate = _abilityFactory.CreateAbilityTemplate(skillsData.SkillIds.Dequeue());
                }
            }

            Debug.Log(this.GetHashCode().ToString());
            OnSkillChanged?.Invoke();
        }
    }
}
