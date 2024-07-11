using System;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class WorldData
    {
        public LootData LootData;
        public WeaponUnlockedData WeaponUnlockedData;
        public LocationProgressData LocationProgressData;
        public WorldData()
        {
            LootData = new LootData();
            WeaponUnlockedData = new WeaponUnlockedData();
            LocationProgressData = new LocationProgressData();
        }
    }
}