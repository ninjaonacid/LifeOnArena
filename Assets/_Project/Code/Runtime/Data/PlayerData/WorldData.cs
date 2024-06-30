using System;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public LootData LootData;
        public WeaponUnlockedData WeaponUnlockedData;
        public LocationProgressData LocationProgressData;
        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            LootData = new LootData();
            WeaponUnlockedData = new WeaponUnlockedData();
            LocationProgressData = new();
        }
    }
}