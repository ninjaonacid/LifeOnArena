using System.Collections.Generic;
using Code.ConfigData;
using Code.ConfigData.Ability;
using Code.ConfigData.Audio;
using Code.ConfigData.Identifiers;
using Code.ConfigData.Levels;
using Code.ConfigData.Settings;
using Code.ConfigData.StatSystem;
using Code.ConfigData.UIWindows;
using Code.UI;

namespace Code.Services.ConfigData
{
    public interface IConfigProvider : IService
    {
        EnemyDataConfig Monster(MobId typeId);
        void Load();
        LevelReward Reward(LocationReward rewardId);
        StatDatabase CharacterStats();
        List<LevelReward> LoadRewards();
        ParticleData Particle(int id);
        LevelConfig Level(string sceneKey);
        ScreenConfig ForWindow(ScreenID menuId);
        AbilityTemplateBase Ability(int heroAbilityId);
        WeaponData Weapon(WeaponId weaponId);
        WeaponPlatformStaticData WeaponPlatforms(WeaponId weaponId);
        List<LevelConfig> LoadLevels();

        AudioLibrary AudioLibrary();
        AudioServiceSettings AudioServiceSettings();
    }
}