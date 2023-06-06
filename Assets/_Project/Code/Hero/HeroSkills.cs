using System;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Infrastructure.InputSystem;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using SimpleInputNamespace;
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
            public ButtonInputUI Button;
            public AbilityTemplateBase AbilityTemplate;
            public AbilitySlotID AbilitySlotID;
        }

        [Inject]
        public void Construct(IAbilityFactory abilityFactory, IInputSystem input)
        {
            _abilityFactory = abilityFactory;
            _input = input;
        }

        // private void Update()
        // {
        //     foreach (var slot in SkillSlots)
        //     {
        //         if (_input.IsButtonPressed(slot.Button.button.Key))
        //         {
        //             if (slot.AbilityTemplate && slot.AbilityTemplate.IsReady())
        //             {
        //                 slot.AbilityTemplate.GetAbility().Use(this.gameObject, null);
        //                 slot.AbilityTemplate.State = AbilityState.Active;
        //                 _activeSkill = slot.AbilityTemplate;
        //                 _heroCooldown.StartCooldown(slot.AbilityTemplate);
        //             }
        //         }
        //     }
        // }

        public void LoadProgress(PlayerProgress progress)
        {
            var skillsData = progress.SkillSlotsData;

            for (var index = 0; index <= skillsData.SkillIds.Count; index++)
            {
                var slot = SkillSlots[index];

                if (skillsData.SkillIds != null && skillsData.SkillIds.Count >= 1)
                {
                    slot.AbilityTemplate = _abilityFactory.CreateAbilityTemplate(skillsData.SkillIds.Dequeue());
                }
            }

            OnSkillChanged?. Invoke();
        }
    }
}
