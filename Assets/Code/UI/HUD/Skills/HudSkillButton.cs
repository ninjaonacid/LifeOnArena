using Code.Data;
using Code.Services;
using Code.StaticData.Ability;
using UnityEngine;

namespace Code.UI.HUD.Skills
{
    public class HudSkillButton : MonoBehaviour
    {
        public AbilityId AbilityId;
        public SkillSlotID SkillSlotID;

        private IStaticDataService _staticData;
        private SkillHudData _skillHudData;
        private HudSkillIcon _skillIcon;
        public void Construct(IStaticDataService staticData, SkillHudData skillHudData)
        {
            _skillIcon = GetComponentInChildren<HudSkillIcon>();

            _staticData = staticData;
            _skillHudData = skillHudData;

            _skillIcon.Image.enabled = false;

            _skillHudData.WeaponSkillChanged += UpdateWeaponSkill;

            if (_skillHudData.SlotSkill.TryGetValue(SkillSlotID, out var abilityId))
            {
                var ability = _staticData.ForAbility(abilityId);
                if (ability == null) return;

                AbilityId = ability.AbilityId;

                _skillIcon.Image.sprite = ability.SkillIcon;

                _skillIcon.Image.enabled = true;
            }
        }

        private void UpdateWeaponSkill(HeroAbility heroAbility)
        {
            if (heroAbility.SkillSlotID == SkillSlotID)
            {
                AbilityId = heroAbility.AbilityId;
                _skillIcon.Image.sprite = heroAbility.SkillIcon;
                _skillIcon.Image.enabled = true;
            }
        }

        private void OnDestroy()
        {
            _skillHudData.WeaponSkillChanged -= UpdateWeaponSkill;
        }



    }
}
