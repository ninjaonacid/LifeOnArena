﻿using System.Collections.Generic;
using System.Linq;
using Code.ConfigData;
using Code.ConfigData.Ability;
using Code.ConfigData.Audio;
using Code.ConfigData.Identifiers;
using Code.ConfigData.Levels;
using Code.ConfigData.Settings;
using Code.ConfigData.StatSystem;
using Code.ConfigData.UIWindows;
using Code.Data.PlayerData;
using Code.UI;
using UnityEngine;

namespace Code.Services.ConfigData
{
    public class ConfigProvider : IConfigProvider
    {
        private const string ConfigFolder = "Configs";
        private Dictionary<int, EnemyDataConfig> _monsters;
        private Dictionary<string, LevelConfig> _levels; 
        private Dictionary<LocationReward, LevelReward> _levelReward;
        private Dictionary<ScreenID, ScreenConfig> _windowConfigs;
        private Dictionary<int, AbilityTemplateBase> _heroAbilities;
        private Dictionary<int, ViewObjectData> _particles;
        private Dictionary<int, WeaponData> _weapons;
        private AudioLibrary _audioLibrary;
        private AudioServiceSettings _audioServiceSettings;
        private StatDatabase _characterStats;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<EnemyDataConfig>($"{ConfigFolder}/Monsters")
                .ToDictionary(x => x.MobId.Id, x => x);

            _audioLibrary = Resources
                .Load<AudioLibrary>("Sounds/AudioLibrary");
            
            _audioServiceSettings = Resources
                .Load<AudioServiceSettings>($"{ConfigFolder}/Audio/AudioServiceSettings");

            _levels = Resources
                .LoadAll<LevelConfig>($"{ConfigFolder}/Levels")
                .ToDictionary(x => x.LevelKey, x => x);

            _levelReward = Resources
                .LoadAll<LevelReward>($"{ConfigFolder}/Levels/LevelReward")
                .ToDictionary(x => x.LocationReward);

            _windowConfigs = Resources
                .Load<WindowsStaticData>($"{ConfigFolder}/UIWindows/WindowsStaticData")
                .Configs
                .ToDictionary(x => x.ScreenID, x => x);

            _heroAbilities = Resources
                .LoadAll<AbilityTemplateBase>($"{ConfigFolder}/Hero/HeroSkills")
                .ToDictionary(x => x.Identifier.Id, x => x);

            _characterStats = Resources
                .Load<StatDatabase>($"{ConfigFolder}/Hero/Stats/HeroStatsData");

            _particles = Resources
                .LoadAll<ViewObjectData>($"{ConfigFolder}/Particles")
                .ToDictionary(x => x.Identifier.Id, x => x);
            
            _weapons = Resources
                .LoadAll<WeaponData>($"{ConfigFolder}/Equipment/Weapons")
                .ToDictionary(x => x.WeaponId.Id, x => x);

        }

        public WeaponData Weapon(int weaponId)
        {
            if(_weapons.TryGetValue(weaponId, out var weaponStaticData))
                return weaponStaticData;

            return null;
        }

        public AudioServiceSettings AudioServiceSettings() => _audioServiceSettings;
        public AudioLibrary AudioLibrary() => _audioLibrary;

        public ViewObjectData Vfx(int id)
        {
            if(_particles.TryGetValue(id, out var particlesStaticData))
                return particlesStaticData;

            return null;
        }
        
        public EnemyDataConfig Monster(int id)
        {
            if (_monsters.TryGetValue(id, out var monsterStaticData))
                return monsterStaticData;

            return null;
        }

        public AbilityTemplateBase Ability(int heroAbilityId)    
        {
            if (_heroAbilities.TryGetValue(heroAbilityId, out var heroAbility))
                return heroAbility;

            return null;
        }
        

        public LevelConfig Level(string sceneKey) =>
        
            _levels.TryGetValue(sceneKey, out LevelConfig staticData)
                ? staticData
                : null;


        public List<LevelConfig> LoadLevels() =>
        _levels.Values.ToList();

        public LevelReward Reward(LocationReward rewardId)
        {
            throw new System.NotImplementedException();
        }

        public StatDatabase CharacterStats() =>
            _characterStats;

        public List<LevelReward> LoadRewards() =>
        _levelReward.Values.ToList();
        
        public ScreenConfig ForWindow(ScreenID menuId) =>
        
            _windowConfigs.TryGetValue(menuId, out ScreenConfig windowConfig)
                ? windowConfig
                : null;
        
    }
}