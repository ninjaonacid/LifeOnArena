using System;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services;
using Code.Services.Input;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.Hero
{
    public class HeroSkills : MonoBehaviour
    {
        public event Action OnSkillChanged;

        [SerializeField] private HeroAbilityCooldown _heroCooldown;
        public HeroAttack HeroAttack;
        public SkillSlot[] SkillSlots;

        private SkillHudData _skillHudData;
        private IAbilityFactory _abilityFactory;
        private IInputService _input;

        [Serializable]
        public class SkillSlot
        {
            public string ButtonKey { get; set; }
            public AbilitySlotID AbilitySlotID;
            public AbilityBluePrintBase Ability;
        }


        public void Construct(IAbilityFactory abilityFactory)
        {
            _abilityFactory = abilityFactory;
        }

        private void Awake()
        {
            _input = ServiceLocator.Container.Single<IInputService>();
        }

        private void Update()
        {
            foreach (var slot in SkillSlots)
            {
                if (_input.IsButtonPressed(slot.ButtonKey))
                {
                    if (slot.Ability && slot.Ability.IsReady())
                    {
                        // slot.Ability.GetAbility().Use();
                        _heroCooldown.StartCooldown(slot.Ability);
                    }
                }

            }
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
