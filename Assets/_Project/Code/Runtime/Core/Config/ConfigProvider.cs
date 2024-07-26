﻿using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Audio;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.ConfigData.Reward;
using Code.Runtime.ConfigData.ScreenUI;
using Code.Runtime.ConfigData.Settings;
using Code.Runtime.ConfigData.StateMachine;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.TutorialService;
using Code.Runtime.UI;
using UnityEngine;

namespace Code.Runtime.Core.Config
{
    public class ConfigProvider
    {
        private const string ConfigFolder = "Configs";
        private Dictionary<int, EnemyDataConfig> _monsters;
        private Dictionary<int, LevelConfig> _levelsByInt;
        private Dictionary<string, LevelConfig> _levelsByString;
        private Dictionary<ScreenID, ScreenConfig> _windowConfigs;
        private Dictionary<int, ActiveAbilityBlueprintBase> _abilities;
        private Dictionary<int, VisualEffectData> _visualEffects;
        private Dictionary<int, WeaponData> _weapons;
        private Dictionary<int, RewardBlueprintBase> _rewards;
        
        private List<ActiveAbilityBlueprintBase> _treeAbilities;
        private List<WeaponData> _heroWeapons;

        private InitialGameConfig _initializeGameConfig;
        private TutorialConfig _tutorialConfig;
        private AudioLibrary _audioLibrary;
        private AudioServiceSettings _audioServiceSettings;
        private AdvertisementConfig _adsConfig;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<EnemyDataConfig>($"{ConfigFolder}/Monsters")
                .ToDictionary(x => x.MobId.Id, x => x);

            _audioLibrary = Resources
                .Load<AudioLibrary>("Sounds/AudioLibrary");
            
            _audioServiceSettings = Resources
                .Load<AudioServiceSettings>($"{ConfigFolder}/Audio/AudioServiceSettings");

            _levelsByInt = Resources
                .LoadAll<LevelConfig>($"{ConfigFolder}/Levels")
                .ToDictionary(x => x.LevelId.Id, x => x);
            
            _levelsByString = Resources
                .LoadAll<LevelConfig>($"{ConfigFolder}/Levels")
                .ToDictionary(x => x.LevelId.Name, x => x);

            _windowConfigs = Resources
                .Load<WindowDatabase>($"{ConfigFolder}/ScreenUI/ScreenDatabase")
                .Configs
                .ToDictionary(x => x.ScreenID, x => x);

            _abilities = Resources
                .LoadAll<ActiveAbilityBlueprintBase>($"{ConfigFolder}/AbilitySystem")
                .ToDictionary(x => x.Identifier.Id, x => x);

            _visualEffects = Resources
                .LoadAll<VisualEffectData>($"{ConfigFolder}/VisualEffects")
                .ToDictionary(x => x.Identifier.Id, x => x);

            _weapons = Resources
                .LoadAll<WeaponData>($"{ConfigFolder}/Equipment/Weapons")
                .ToDictionary(x => x.WeaponId.Id, x => x);
            
            _heroWeapons = _weapons.Values
                .Where(x => x.IsHeroWeapon)
                .ToList();
            
            _treeAbilities = _abilities.Values
                .Where(x => x.AbilityTreeData != null 
            && x.AbilityTreeData.Branch != AbilityTreeBranch.NotInTree)
                .ToList(); 

            _rewards = Resources
                .LoadAll<RewardBlueprintBase>($"{ConfigFolder}/Rewards")
                .ToDictionary(x => x.RewardId.Id, x => x);

            _initializeGameConfig = Resources
                .Load<InitialGameConfig>($"{ConfigFolder}/InitialGameConfig");

            _adsConfig = Resources.Load<AdvertisementConfig>($"{ConfigFolder}/AdvertisementConfig");

            _tutorialConfig = Resources.Load<TutorialConfig>($"{ConfigFolder}/Tutorial/StartGameTutorial");

        }

        public InitialGameConfig GetInitialConfig() => _initializeGameConfig; 
        public WeaponData Weapon(int weaponId)
        {
            if(_weapons.TryGetValue(weaponId, out var weaponStaticData))
                return weaponStaticData;

            return null;
        }

        public AudioServiceSettings AudioServiceSettings() => _audioServiceSettings;
        public AudioLibrary AudioLibrary() => _audioLibrary;

        public VisualEffectData VisualEffect(int id)
        {
            if(_visualEffects.TryGetValue(id, out var particlesStaticData))
                return particlesStaticData;

            return null;
        }

        public TutorialConfig GetTutorialConfig() => _tutorialConfig;
        public EnemyDataConfig Monster(int id)
        {
            if (_monsters.TryGetValue(id, out var monsterStaticData))
                return monsterStaticData;

            return null;
        }

        public ActiveAbilityBlueprintBase Ability(int heroAbilityId)    
        {
            if (_abilities.TryGetValue(heroAbilityId, out var heroAbility))
                return heroAbility;

            return null;
        }

        public List<ActiveAbilityBlueprintBase> AllAbilities()
        {
            if (_abilities.Count > 0)
            {
                return _abilities.Values.ToList();
            }

            return null;
        }

        public List<ActiveAbilityBlueprintBase> GetTreeAbilities() => _treeAbilities;

        public List<WeaponData> GetHeroWeapons() => _heroWeapons;
        public LevelConfig Level(int configId) =>
        
            _levelsByInt.TryGetValue(configId, out LevelConfig staticData)
                ? staticData
                : null;
        
        public LevelConfig Level(string configId) =>
        
            _levelsByString.TryGetValue(configId, out LevelConfig staticData)
                ? staticData
                : null;
        
        public List<LevelConfig> LoadLevels() =>
        _levelsByInt.Values.ToList();

        public RewardBlueprintBase Reward(int id) =>
            _rewards.TryGetValue(id, out RewardBlueprintBase reward) ? reward : null;

        public ScreenConfig ForWindow(ScreenID menuId) =>
        
            _windowConfigs.TryGetValue(menuId, out ScreenConfig windowConfig)
                ? windowConfig
                : null;


        public AdvertisementConfig GetAdsConfig() => _adsConfig;
    }
}