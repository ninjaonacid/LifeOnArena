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
        public TutorialData TutorialData;
        public RewardsData RewardsData;

        public PlayerData()
    {
        WorldData = new WorldData();
        HeroEquipment = new HeroEquipment();
        KillData = new KillData();
        AbilityData = new AbilityData();
        PassiveSkills = new PassiveSkills();
        StatsData = new StatsData();
        PlayerExp = new PlayerExp();
        TutorialData = new TutorialData();
        RewardsData = new RewardsData();
    }
        
    }
}