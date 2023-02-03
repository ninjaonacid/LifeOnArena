using System;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.Input;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.Hero
{
    public class HeroSkills : MonoBehaviour
    {
        public event Action OnSkillChanged;

        public HeroAttack HeroAttack;
        public SkillSlot[] SkillSlots;
   
        private SkillHudData _skillHudData;
        private IAbilityFactory _abilityFactory;
        private IInputService _input;

        [Serializable]
        public class SkillSlot
        {
            public AbilitySlotID AbilitySlotID;
            public AbilityBluePrintBase Ability;
            public string ButtonKey { get; set; }
        }


        public void Construct(IAbilityFactory abilityFactory)
        {
            _abilityFactory = abilityFactory;
        }

        public void ChangeSkill(AbilityBluePrintBase heroAbility)
        {
            foreach (var slot in SkillSlots)
            {
                if (heroAbility.abilitySlot == slot.AbilitySlotID)
                {
                    slot.Ability = _abilityFactory.CreateAbility(heroAbility.Id);
                    HeroAttack.SetActiveSkill(heroAbility);
                }
            }

            OnSkillChanged?.Invoke();
        }

        public SkillSlot GetWeaponSkillSlot()
        {
            foreach (var slot in SkillSlots)
            {
                if (slot.AbilitySlotID == AbilitySlotID.WeaponSkillSlot)
                {
                    return slot;
                }
            }

            return null;
        }

        public SkillSlot GetDodgeSkillSlot()
        {
            foreach (var slot in SkillSlots)
            {
                if (slot.AbilitySlotID == AbilitySlotID.Dodge)
                {
                    return slot;
                }
            }

            return null;
        }
    }
}
