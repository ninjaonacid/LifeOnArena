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
        public SkillsData SkillsData;
        public PassiveSkills PassiveSkills;
        public SkillSlotsData SkillSlotsData;
        
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroEquipment = new HeroEquipment();
            CharacterStats = new CharacterStats();
            KillData = new KillData();
            SkillsData = new SkillsData();
            SkillSlotsData = new SkillSlotsData();
            PassiveSkills = new PassiveSkills();
        }
    }
}