using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/HeroAbility", fileName = "HeroSkill")]
    public class HeroAbility_SO : ScriptableObject
    {
        public float Cooldown;
        public float Damage;

    }
}
