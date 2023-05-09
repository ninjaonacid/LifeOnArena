using System;


namespace Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public CharacterStats CharacterStats;
        public HeroEquipment HeroEquipment;
        public KillData KillData;
        public WorldData WorldData;
        public PassiveSkills PassiveSkills;
        public SkillSlotsData SkillSlotsData;
        public CharacterStatsData CharacterStatsData;
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroEquipment = new HeroEquipment();
            CharacterStats = new CharacterStats();
            KillData = new KillData();
            SkillSlotsData = new SkillSlotsData();
            PassiveSkills = new PassiveSkills();
            CharacterStatsData = new CharacterStatsData();
        }
    }
}