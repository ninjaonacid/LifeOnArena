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
    public class HeroSkills : MonoBehaviour, ISavedProgress
    {
        public event Action OnSkillChanged;

        public SkillHudData SkillHudData;

        public SkillSlot[] SkillSlots;

        private IAbilityFactory _abilityFactory;
        [Serializable]
        public class SkillSlot
        {
            public SkillSlotID SkillSlotId;
            public AbilityId AbilityId;
            public Ability Ability;
        }

        public void Construct(IAbilityFactory abilityFactory)
        {
            _abilityFactory = abilityFactory;
        }
    
        public void ChangeWeaponSkill(HeroAbilityData heroAbilityData)
        {
            foreach (var slot in SkillSlots)
            {
                if (heroAbilityData.SkillSlotID == slot.SkillSlotId)
                {
                    slot.AbilityId = heroAbilityData.AbilityId;
                    slot.Ability = _abilityFactory.CreateAbility(heroAbilityData.AbilityId);
                    OnSkillChanged?.Invoke();
                }
            }

            OnSkillChanged?.Invoke();
        }

        private void LoadAbilities()
        {
            foreach (var slot in SkillSlots)
            {
               slot.Ability =  _abilityFactory.CreateAbility(slot.AbilityId);
               
            }

            OnSkillChanged?.Invoke();
        }
    
        public void LoadProgress(PlayerProgress progress)
        {
            SkillHudData = progress.skillHudData;

            foreach (var slot in SkillSlots)
            {
                if(SkillHudData.SlotSkill.TryGetValue(slot.SkillSlotId, out var abilityId))
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
                if(SkillHudData.SlotSkill.TryAdd(slot.SkillSlotId, slot.AbilityId))
                {
                    SkillHudData.SlotSkill[slot.SkillSlotId] = slot.AbilityId;
                }
            }
        }
    }
}
