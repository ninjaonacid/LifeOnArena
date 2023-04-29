using System;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.Input;
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
        [SerializeField] private HeroStateMachineHandler _heroStateMachine;
        public HeroAttack HeroAttack;
        public SkillSlot[] SkillSlots;
        public AbilityTemplateBase ActiveSkill => _activeSkill;

        private SkillSlotsData _skillSlotsData;
        private AbilityTemplateBase _activeSkill;
        private IAbilityFactory _abilityFactory;
        private IInputService _input;

        [Serializable]
        public class SkillSlot
        {
            public string ButtonKey { get; set; }
            public AbilitySlotID AbilitySlotID;
            public AbilityTemplateBase Ability;
        }

        [Inject]
        public void Construct(IAbilityFactory abilityFactory, IInputService input)
        {
            _abilityFactory = abilityFactory;
            _input = input;
        }

        private void Update()
        {
            foreach (var slot in SkillSlots)
            {
                if (_input.IsButtonPressed(slot.ButtonKey))
                {
                    if (slot.Ability && slot.Ability.IsReady())
                    {
                        slot.Ability.GetAbility().Use(this.gameObject, null);
                        slot.Ability.State = AbilityState.Active;
                        _activeSkill = slot.Ability;
                        _heroCooldown.StartCooldown(slot.Ability);
                    }
                }
            }
        }

        
        public bool IsSkillButtonPressed()
        {
            foreach (var slot in SkillSlots)
            {
                return _input.IsButtonPressed(slot.ButtonKey);
            }

            return false;
        }
        //public void ChangeSkill(AbilityTemplateBase heroAbility)
        //{
        //    foreach (var slot in SkillSlots)
        //    {
        //        if (heroAbility.AbilitySlot == slot.AbilitySlotID)
        //        {
        //            slot.Ability = _abilityFactory.CreateAbility(heroAbility.Id);
        //            HeroAttack.SetActiveSkill(heroAbility);
        //        }
        //    }

        //    OnSkillChanged?.Invoke();
        //}


        public void LoadProgress(PlayerProgress progress)
        {
            var skillsData = progress.SkillSlotsData.SlotSkill;

            foreach (var slot in SkillSlots)
            {
                if (skillsData[slot.AbilitySlotID] != null)
                    slot.Ability = _abilityFactory.CreateAbility(skillsData[slot.AbilitySlotID]);
            }
            OnSkillChanged?. Invoke();
        }
    }
}
