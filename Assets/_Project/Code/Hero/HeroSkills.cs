using System;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.InputSystem;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Hero
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
        private IInputSystem _input;


        [Serializable]
        public class SkillSlot
        {
            public AbilityTemplateBase AbilityTemplate;
            public AbilitySlotID AbilitySlotID;
        }

        [Inject]
        public void Construct(IAbilityFactory abilityFactory, IInputSystem input)
        {
            _abilityFactory = abilityFactory;
            _input = input;
        }

        private void Start()
        {
            _input.Player.SkillSlot1.performed += OnSkillSlot1;
            _input.Player.SkillSlot2.performed += OnSkillSlot2;
        }

        private void OnDestroy()
        {
            _input.Player.SkillSlot1.performed -= OnSkillSlot1;
            _input.Player.SkillSlot2.performed -= OnSkillSlot2;
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
        
            for (var index = 0; index <= skillsData.SkillIds.Count; index++)
            {
                var slot = SkillSlots[index];
                
                slot.AbilityTemplate = _abilityFactory.CreateAbilityTemplate(skillsData.SkillIds.Dequeue());
                
            }
            Debug.Log(this.GetHashCode().ToString());
            OnSkillChanged?.Invoke();
        }
    }
}
