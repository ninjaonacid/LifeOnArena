using System;

namespace Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HeroHP heroHeroHp;
        public CharacterStats CharacterStats;
        public KillData KillData;
        public WorldData WorldData;
        public SkillsData SkillsData;
        public SkillHolderData SkillHolderData;
        public InventoryData InventoryData;
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            heroHeroHp = new HeroHP();
            CharacterStats = new CharacterStats();
            KillData = new KillData();
            InventoryData = new InventoryData();
            SkillsData = new SkillsData();
            SkillHolderData = new SkillHolderData();
        }
    }
}