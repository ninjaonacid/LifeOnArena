using System;

namespace Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HeroHP HeroHp;
        public CharacterStats CharacterStats;
        public HeroEquipment HeroEquipment;
        public KillData KillData;
        public WorldData WorldData;
        public SkillsData SkillsData;
        public SkillHudData skillHudData;
        public InventoryData InventoryData;
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroHp = new HeroHP();
            HeroEquipment = new HeroEquipment();
            CharacterStats = new CharacterStats();
            KillData = new KillData();
            InventoryData = new InventoryData();
            SkillsData = new SkillsData();
            skillHudData = new SkillHudData();
        }
    }
}