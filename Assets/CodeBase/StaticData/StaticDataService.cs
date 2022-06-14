using System.Collections.Generic;
using System.Linq;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId)
        {
            if (_monsters.TryGetValue(typeId, out var monsterStaticData))
                return monsterStaticData;

            return null;
        }

        public LevelStaticData ForLevel(string sceneKey) =>
        
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;
        
    }
}