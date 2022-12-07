using System;
using System.Collections.Generic;
using Code.Data;
using Code.Hero.Abilities;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.Hero
{
    public class HeroSkills : MonoBehaviour
    {
        public Dictionary<AbilityId, HeroAbility> HeroAbilities;
        public AbilityHolder AbilityHolder;
        public SkillHudData SkillHudData;

        public SkillSlotID WeaponSkill;
        public SkillSlotID DodgeSkill;
        public SkillSlotID RageSkill;


        private IProgressService _progress;
        private IStaticDataService _staticData;
        public void Construct(IStaticDataService staticData, IProgressService progressService)
        {
            _staticData = staticData;
            _progress = progressService;
            
            _staticData.ForAbility(_progress.Progress.skillHudData.SlotSkill[WeaponSkill]);
        }
        private void Awake()
        {
            HeroAbilities = new Dictionary<AbilityId, HeroAbility>();
            _progress = AllServices.Container.Single<IProgressService>();
        }

    }
}
