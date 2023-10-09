using System;
using UnityEngine.Serialization;

namespace Code.Data.PlayerData
{
    [Serializable]
    public class PlayerData
    {
        public PlayerExp PlayerExp;
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
        PlayerExp = new PlayerExp();
    }
    }
}