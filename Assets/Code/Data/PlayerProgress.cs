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
        public SkillHudData skillHudData;
  
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroEquipment = new HeroEquipment();
            CharacterStats = new CharacterStats();
            KillData = new KillData();
            SkillsData = new SkillsData();
            skillHudData = new SkillHudData();
        }
    }
}