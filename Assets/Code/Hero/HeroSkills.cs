using System;
using Code.Data;
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
            public HeroAbilityId heroAbilityId;
            public HeroAbilityData ability;
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
                    return SkillSlots[i].ability;
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
                    slot.heroAbilityId = heroAbilityData.HeroAbilityId;
                    slot.ability = _abilityFactory.CreateAbility(heroAbilityData.HeroAbilityId);
                    HeroAttack.SetActiveSkill(heroAbilityData);
                }
            }

            OnSkillChanged?.Invoke();
        }

        private void LoadAbilities()
        {
            foreach (var slot in SkillSlots)
            {
                slot.ability = _abilityFactory.CreateAbility(slot.heroAbilityId);

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
                    slot.heroAbilityId = abilityId;
                }
            }

            LoadAbilities();

        }

        public void UpdateProgress(PlayerProgress progress)
        {
            foreach (var slot in SkillSlots)
            {
                if (_skillHudData.SlotSkill.TryAdd(slot.SkillSlotId, slot.heroAbilityId))
                {
                    _skillHudData.SlotSkill[slot.SkillSlotId] = slot.heroAbilityId;
                }
            }
        }
    }
}
