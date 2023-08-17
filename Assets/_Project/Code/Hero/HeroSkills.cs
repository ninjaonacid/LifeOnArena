using System;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.InputSystem;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using UnityEngine;
using VContainer;

namespace Code.Hero
{
    public class HeroSkills : MonoBehaviour, ISaveReader
    {
        public event Action OnSkillChanged;

        [SerializeField] private HeroAbilityCooldown _heroCooldown;
      
        public SkillSlot[] SkillSlots;
        private SkillSlotsData _skillSlotsData;

        private AbilityTemplateBase _activeSkill;
        public AbilityTemplateBase ActiveSkill => _activeSkill;
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

        private void Update()
        {
            foreach (var slot in SkillSlots)
            {
                if (_input.Player.SkillSlot1.triggered || _input.Player.SkillSlot2.triggered)
                {
                    if (slot.AbilityTemplate && slot.AbilityTemplate.IsReady())
                    {
                        slot.AbilityTemplate.GetAbility().Use(this.gameObject, null);
                        slot.AbilityTemplate.State = AbilityState.Active;
                        _activeSkill = slot.AbilityTemplate;
                        _heroCooldown.StartCooldown(slot.AbilityTemplate);
                    }
                }
            }
        }

        // public void LoadData(PlayerData data)
        // {
        //     var tornadoId = -455526352;
        //     SkillSlots[0].AbilityTemplate = _abilityFactory.CreateAbilityTemplate(tornadoId);
        //     OnSkillChanged?.Invoke();
        // }

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
