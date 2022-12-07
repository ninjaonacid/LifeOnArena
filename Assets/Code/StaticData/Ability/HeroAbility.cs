using Code.Hero.HeroStates;
using Code.UI.HUD.Skills;
using UnityEngine;

namespace Code.StaticData.Ability
{
    [CreateAssetMenu(menuName = "StaticData/HeroAbility", fileName = "HeroSkill")]
    public class HeroAbility : ScriptableObject
    {
        public float Cooldown;
        public float Damage;
        public Sprite SkillIcon;
        public HeroBaseState State;
        public AbilityId AbilityId;
        public SkillSlotID SkillSlotID;

    }
}
