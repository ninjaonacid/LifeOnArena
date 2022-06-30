using System;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State HeroState;
        public CharacterStats CharacterStats;
        public KillData KillData;
        public WorldData WorldData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new State();
            CharacterStats = new CharacterStats();
            KillData = new KillData();
        }
    }
}