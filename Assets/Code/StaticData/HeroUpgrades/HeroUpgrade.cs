using UnityEngine;

namespace Code.StaticData.HeroUpgrades
{
    public enum UpgradeRarity
    {
        Common,
        Rare,
        Legendary
    }
   
    public abstract class HeroUpgrade : ScriptableObject
    {
        public UpgradeRarity UpgradeRarity;
        public Sprite UpgradeIcon;
        public Sprite UpgradeRarityIcon;

    }
}
