using System;

namespace Code.Data.PlayerData
{
    [Serializable]
    public class PlayerData
    {
        public HeroExp HeroExp;
        public KillData KillData;
        public WorldData WorldData;
        public SkillSlotsData SkillSlotsData;
        public HeroEquipment HeroEquipment;
        public PassiveSkills PassiveSkills;
        public StatsData StatsData;

        public PlayerData(string initialLevel)
    {
        WorldData = new WorldData(initialLevel);
        HeroEquipment = new HeroEquipment();
        KillData = new KillData();
        SkillSlotsData = new SkillSlotsData();
        PassiveSkills = new PassiveSkills();
        StatsData = new StatsData();
        HeroExp = new HeroExp();
    }
    }
}