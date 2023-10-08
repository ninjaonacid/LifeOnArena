using System;

namespace Code.Data.PlayerData
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public LootData LootData;
        public WeaponPurchaseData WeaponPurchaseData;
        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            LootData = new LootData();
            WeaponPurchaseData = new WeaponPurchaseData();
        }
    }
}