using System;
using Code.Data;
using Code.Hero.Abilities;
using Code.Infrastructure.Factory;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.Hero
{
    public class HeroSkills : MonoBehaviour, ISave
    {
        public event Action OnSkillChanged;

        public HeroAttack HeroAttack;
        public SkillSlot[] SkillSlots;

        private SkillHudData _skillHudData;
        private IAbilityFactory _abilityFactory;



        [Serializable]
        public class SkillSlot
        {
            public SkillSlotID SkillSlotId;
            public AbilityId AbilityId;
            public HeroAbilityData Ability;
        }

        public void Construct(IAbilityFactory abilityFactory)
        {
            _abilityFactory = abilityFactory;
        }

        public HeroAbilityData GetSkillSlotAbility(SkillSlotID skillSlot)
        {
            for (int i = 0; i < SkillSlots.Length; i++)
            {
                if (SkillSlots[i].SkillSlotId == skillSlot)
                {
                    return SkillSlots[i].Ability;
                }
            }
            return null;
        }


        public void ChangeWeaponSkill(HeroAbilityData heroAbilityData)
        {
            foreach (var slot in SkillSlots)
            {
                if (heroAbilityData.SkillSlotID == slot.SkillSlotId)
                {
                    slot.AbilityId = heroAbilityData.AbilityId;
                    slot.Ability = _abilityFactory.CreateAbility(heroAbilityData.AbilityId);
                    HeroAttack.SetActiveSkill(heroAbilityData);
                }
            }

            OnSkillChanged?.Invoke();
        }

        private void LoadAbilities()
        {
            foreach (var slot in SkillSlots)
            {
                slot.Ability = _abilityFactory.CreateAbility(slot.AbilityId);

            }

            OnSkillChanged?.Invoke();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _skillHudData = progress.skillHudData;

            foreach (var slot in SkillSlots)
            {
                if (_skillHudData.SlotSkill.TryGetValue(slot.SkillSlotId, out var abilityId))
                {
                    slot.AbilityId = abilityId;
                }
            }

            LoadAbilities();

        }

        public void UpdateProgress(PlayerProgress progress)
        {
            foreach (var slot in SkillSlots)
            {
                if (_skillHudData.SlotSkill.TryAdd(slot.SkillSlotId, slot.AbilityId))
                {
                    _skillHudData.SlotSkill[slot.SkillSlotId] = slot.AbilityId;
                }
            }
        }
    }
}
