using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.StaticData.Ability
{
    [CreateAssetMenu(menuName = "StaticData/HeroAttack/HeroAbility", fileName = "HeroSkill")]
    public class HeroAbilityData : Ability
    {
        public float Damage;
        public float Cooldown;
        public float CurrentCooldown;
        public HeroAbilityId HeroAbilityId;
        public SkillSlotID SkillSlotID;

        public bool IsAbilityReady() =>
            CurrentCooldown <= 0;

        private void OnEnable()
        {
            CurrentCooldown = 0;
        }
    }
}
