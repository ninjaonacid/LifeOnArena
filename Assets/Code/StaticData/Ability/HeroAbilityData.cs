using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.StaticData.Ability
{
    [CreateAssetMenu(menuName = "StaticData/HeroAttack/HeroAbility", fileName = "HeroSkill")]
    public class HeroAbilityData : AttackDefinition
    {
        public float Cooldown;
        public float CurrentCooldown;
        public Sprite SkillIcon;
        public AbilityId AbilityId;
        public SkillSlotID SkillSlotID;

        public bool IsAbilityReady() =>
            CurrentCooldown <= 0;

    }
}
