using System.Collections.Generic;
using System.Linq;
using Code.ConfigData;
using Code.ConfigData.Ability;
using Code.ConfigData.Audio;
using Code.ConfigData.Identifiers;
using Code.ConfigData.Levels;
using Code.ConfigData.StatSystem;
using Code.ConfigData.UIWindows;
using Code.UI;
using UnityEngine;

namespace Code.Services.ConfigData
{
    public class ConfigProvider : IConfigProvider
    {
        private Dictionary<MobId, EnemyDataConfig> _monsters;
        private Dictionary<string, LevelConfig> _levels; 
        private Dictionary<LocationReward, LevelReward> _levelReward;
        private Dictionary<ScreenID, ScreenConfig> _windowConfigs;
        private Dictionary<int, AbilityTemplateBase> _heroAbilities;
        private Dictionary<ParticleId, ParticlesStaticData> _particles;
        private Dictionary<WeaponId, WeaponData> _weapons;
        private Dictionary<WeaponId, WeaponPlatformStaticData> _weaponPlatforms;
        private AudioLibrary _audioLibrary;
        private StatDatabase _characterStats;

        public void Load()
        {
            _monsters = Resources
                .LoadAll<EnemyDataConfig>("ConfigData/Monsters")
                .ToDictionary(x => x.MobId, x => x);

            _audioLibrary = Resources
                .Load<AudioLibrary>("ConfigData/Audio/AudioLibrary");

            _levels = Resources
                .LoadAll<LevelConfig>("ConfigData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);

            _levelReward = Resources
                .LoadAll<LevelReward>("ConfigData/Levels/LevelReward")
                .ToDictionary(x => x.LocationReward);

            _windowConfigs = Resources
                .Load<WindowsStaticData>("ConfigData/UIWindows/WindowsStaticData")
                .Configs
                .ToDictionary(x => x.ScreenID, x => x);

            _heroAbilities = Resources
                .LoadAll<AbilityTemplateBase>("ConfigData/Hero/HeroSkills")
                .ToDictionary(x => x.Identifier.Id, x => x);

            _characterStats = Resources
                .Load<StatDatabase>("ConfigData/Hero/Stats/HeroStatsData");

            _particles = Resources
                .LoadAll<ParticlesStaticData>("ConfigData/Particles")
                .ToDictionary(x => x.ParticleId, x => x);
            
            _weapons = Resources
                .LoadAll<WeaponData>("ConfigData/Equipment/Weapons")
                .ToDictionary(x => x.WeaponId, x => x);

            _weaponPlatforms = Resources
                .LoadAll<WeaponPlatformStaticData>("ConfigData/WeaponPlatforms")
                .ToDictionary(x => x.WeaponPlatformId, x => x);

        }

        public WeaponData ForWeapon(WeaponId weaponId)
        {
            if(_weapons.TryGetValue(weaponId, out var weaponStaticData))
                return weaponStaticData;

            return null;
        }

        public AudioLibrary GetLibrary() => _audioLibrary;
     
        public WeaponPlatformStaticData ForWeaponPlatforms(WeaponId weaponId)
        {
            if(_weaponPlatforms.TryGetValue(weaponId, out var weaponPlatformStaticData ))
                return weaponPlatformStaticData;

            return null;
        }

        public ParticlesStaticData ForParticle(ParticleId id)
        {
            if(_particles.TryGetValue(id, out var particlesStaticData))
                return particlesStaticData;

            return null;
        }

        public EnemyDataConfig ForMonster(MobId typeId)
        {
            if (_monsters.TryGetValue(typeId, out var monsterStaticData))
                return monsterStaticData;

            return null;
        }

        public AbilityTemplateBase ForAbility(int heroAbilityId)    
        {
            if (_heroAbilities.TryGetValue(heroAbilityId, out var heroAbility))
                return heroAbility;

            return null;
        }
        

        public LevelConfig ForLevel(string sceneKey) =>
        
            _levels.TryGetValue(sceneKey, out LevelConfig staticData)
                ? staticData
                : null;


        public List<LevelConfig> LoadLevels() =>
        _levels.Values.ToList();

        public LevelReward ForReward(LocationReward rewardId)
        {
            throw new System.NotImplementedException();
        }

        public StatDatabase ForCharacterStats() =>
            _characterStats;

        public List<LevelReward> LoadRewards() =>
        _levelReward.Values.ToList();
        
        public ScreenConfig ForWindow(ScreenID menuId) =>
        
            _windowConfigs.TryGetValue(menuId, out ScreenConfig windowConfig)
                ? windowConfig
                : null;
        
    }
}