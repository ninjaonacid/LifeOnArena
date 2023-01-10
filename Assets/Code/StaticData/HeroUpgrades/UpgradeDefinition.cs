using UnityEngine;

namespace Code.StaticData.HeroUpgrades
{
   
    [CreateAssetMenu(menuName = "StaticData/HeroUpgrade", fileName = "Upgrade")]
    public class UpgradeDefinition : HeroUpgrade
    {
        public float Attack;
        public float Health;
        public float Armor;
    }
}
