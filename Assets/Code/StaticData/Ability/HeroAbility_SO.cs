using Code.Hero.HeroStates;
using UnityEngine;

namespace Code.StaticData.Ability
{
    [CreateAssetMenu(menuName = "StaticData/HeroAbility", fileName = "HeroSkill")]
    public class HeroAbility_SO : ScriptableObject
    {
        public float Cooldown;
        public float Damage;
        public Sprite SkillIcon;
        public AbilityId AbilityId;
    }
}
