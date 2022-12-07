using System.Collections.Generic;
using System.Linq;
using Code.Services;
using Code.StaticData.Ability;
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
        private Dictionary<AbilityId, HeroAbility> _heroAbilities;
        private Dictionary<ParticleId, ParticlesStaticData> _particles;
        private Dictionary<WeaponId, WeaponData> _weapons;

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

            _heroAbilities = Resources
                .LoadAll<HeroAbility>("StaticData/HeroSkills")
                .ToDictionary(x => x.AbilityId, x => x);

            _particles = Resources
                .LoadAll<ParticlesStaticData>("StaticData/Particles")
                .ToDictionary(x => x.ParticleId, x => x);

            _weapons = Resources
                .LoadAll<WeaponData>("StaticData/Equipment/Weapons")
                .ToDictionary(x => x.WeaponId, x => x);
        }

        public WeaponData ForWeapon(WeaponId weaponId)
        {
            if(_weapons.TryGetValue(weaponId, out var weaponStaticData))
                return weaponStaticData;

            return null;
        }

        public ParticlesStaticData ForParticle(ParticleId id)
        {
            if(_particles.TryGetValue(id, out var particlesStaticData))
                return particlesStaticData;

            return null;
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId)
        {
            if (_monsters.TryGetValue(typeId, out var monsterStaticData))
                return monsterStaticData;

            return null;
        }

        public HeroAbility ForAbility(AbilityId abilityId)
        {
            if(_heroAbilities.TryGetValue(abilityId, out var heroAbility))
                return heroAbility;

            return null;
        }

        public Dictionary<AbilityId, HeroAbility> AbilityList()
        {
            return _heroAbilities;
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