using Code.StaticData.Ability;
using UnityEngine;

namespace Code.StaticData.HeroUpgrades
{
   
    [CreateAssetMenu(menuName = "StaticData/PowerUp", fileName = "Stats")]
    public class Stats : ScriptableObject
    {
        [Header("Modifiers to multiply by stats, 1 = 100%")]
        [Range(0.0f, 1f)] public float AttackModifier;
        [Range(0.0f, 1f)] public float HealthModifier;
        [Range(0.0f, 1f)] public float ArmorModifier;
    }
}
