using System.Collections.Generic;
using System.Linq;
using Code.Services;
using Code.StaticData.UIWindows;
using Code.UI;
using UnityEngine;

namespace Code.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels; 
        private Dictionary<UIWindowID, WindowConfig> _windowConfigs;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
            
            _windowConfigs = Resources
                .Load<WindowsStaticData>("StaticData/UIWindows/WindowsStaticData")
                .Configs
                .ToDictionary(x => x.WindowId, x => x);
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

        public WindowConfig ForWindow(UIWindowID menuId) =>
        
            _windowConfigs.TryGetValue(menuId, out WindowConfig windowConfig)
                ? windowConfig
                : null;
        
    }
}