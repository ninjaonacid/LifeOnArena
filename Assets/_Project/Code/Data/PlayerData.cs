using System;

namespace Code.Data
{
    [Serializable]
    public class PlayerData
    {
        public HeroEquipment HeroEquipment;
        public KillData KillData;
        public WorldData WorldData;
        public PassiveSkills PassiveSkills;
        public SkillSlotsData SkillSlotsData;
        public StatsData StatsData;
        
        public PlayerData(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroEquipment = new HeroEquipment();
            KillData = new KillData();
            SkillSlotsData = new SkillSlotsData();
            PassiveSkills = new PassiveSkills();
            StatsData = new StatsData();
        }
    }
}