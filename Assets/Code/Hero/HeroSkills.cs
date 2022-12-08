using System;
using System.Collections.Generic;
using Code.Data;
using Code.Hero.Abilities;
using Code.Infrastructure.Factory;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Code.Hero
{
    public class HeroSkills : MonoBehaviour, ISavedProgress
    {
        public event Action OnSkillChanged;

        public SkillHudData SkillHudData;

        public SkillSlot WeaponSkill;
        public SkillSlot DodgeSkill;
        public SkillSlot RageSkill;

        private IAbilityFactory _abilityFactory;
        [Serializable]
        public class SkillSlot
        {
            public SkillSlotID skillSlotId;
            public AbilityId abilityId;
            public HeroAbilityData abilityData;
        }


        public void Construct(IAbilityFactory abilityFactory)
        {
            _abilityFactory = abilityFactory;
        }
        private void Awake()
        {

        }

        public void ChangeWeaponSkill(HeroAbilityData heroAbilityData)
        {
            WeaponSkill.abilityData = heroAbilityData;
            WeaponSkill.abilityId = heroAbilityData.AbilityId;

            OnSkillChanged?.Invoke();
        }

        private void LoadAbilities()
        {
            _abilityFactory.CreateAbility(WeaponSkill.abilityId);
            _abilityFactory.CreateAbility(DodgeSkill.abilityId);
            _abilityFactory.CreateAbility(RageSkill.abilityId);
        }
    
        public void LoadProgress(PlayerProgress progress)
        {
            SkillHudData = progress.skillHudData;
            if (SkillHudData.SlotSkill.TryGetValue(WeaponSkill.skillSlotId, out var weaponAbility))
            {
                WeaponSkill.abilityId = weaponAbility;
            };
            if (SkillHudData.SlotSkill.TryGetValue(DodgeSkill.skillSlotId, out var dodgeAbility))
            {
                DodgeSkill.abilityId = dodgeAbility;
            };
            if (SkillHudData.SlotSkill.TryGetValue(WeaponSkill.skillSlotId, out var rageAbility))
            {
                RageSkill.abilityId = rageAbility;
            };

            LoadAbilities();

        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (SkillHudData.SlotSkill.TryAdd(WeaponSkill.skillSlotId, WeaponSkill.abilityId))
            {
                SkillHudData.SlotSkill[WeaponSkill.skillSlotId] = WeaponSkill.abilityId;
            };

            if (SkillHudData.SlotSkill.TryAdd(DodgeSkill.skillSlotId, DodgeSkill.abilityId))
            {
                SkillHudData.SlotSkill[DodgeSkill.skillSlotId] = DodgeSkill.abilityId;
            };

            if(SkillHudData.SlotSkill.TryAdd(RageSkill.skillSlotId, RageSkill.abilityId))
            {
                SkillHudData.SlotSkill[RageSkill.skillSlotId] = RageSkill.abilityId;
            }
        }
    }
}
