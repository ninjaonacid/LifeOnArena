using System;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class PlayerData
    {
        public PlayerExp PlayerExp;
        public KillData KillData;
        public WorldData WorldData;
        public AbilityData AbilityData;
        public HeroEquipment HeroEquipment;
        public PassiveSkills PassiveSkills;
        public StatsData StatsData;

        public PlayerData(string initialLevel)
    {
        WorldData = new WorldData(initialLevel);
        HeroEquipment = new HeroEquipment();
        KillData = new KillData();
        AbilityData = new AbilityData();
        PassiveSkills = new PassiveSkills();
        StatsData = new StatsData();
        PlayerExp = new PlayerExp();
    }
        
    }
}