using System;
using System.Collections.Generic;
using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.Hero
{
    public class HeroSkills : MonoBehaviour, ISavedProgress
    {
        public event Action OnSkillChanged;
        public Dictionary<AbilityId, HeroAbility> HeroAbilities;

        public SkillHudData SkillHudData;


        public SkillSlot[] SkillSlots;

        [Serializable]
        public class SkillSlot
        {
            public SkillSlotID skillSlotId;
            public HeroAbility ability;
        }
        private IStaticDataService _staticData;
        public void Construct(IStaticDataService staticData)
        {
            _staticData = staticData;
            
        }
        private void Awake()
        {
            HeroAbilities = new Dictionary<AbilityId, HeroAbility>();
        }

        public void ChangeSkill(HeroAbility heroAbility)
        {
            foreach (var skillSlot in SkillSlots)
            {
                if (heroAbility.SkillSlotID == skillSlot.skillSlotId)
                {
                    skillSlot.ability = heroAbility;
                    OnSkillChanged?.Invoke();
                }
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            SkillHudData = progress.skillHudData;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}
