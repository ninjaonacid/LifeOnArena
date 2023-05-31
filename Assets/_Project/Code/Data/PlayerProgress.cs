using System;


namespace Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
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
            KillData = new KillData();
            SkillSlotsData = new SkillSlotsData();
            PassiveSkills = new PassiveSkills();
            CharacterStatsData = new CharacterStatsData();
        }
    }
}