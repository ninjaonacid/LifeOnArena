using System;
using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.StaticData.Ability
{
    [CreateAssetMenu(menuName = "StaticData/HeroAttack/HeroAbility", fileName = "HeroSkill")]
    public class HeroAbilityData : AttackDefinition
    {
        public float Cooldown;
        public Sprite SkillIcon;
        public AbilityId AbilityId;
        public SkillSlotID SkillSlotID;
    }
}
