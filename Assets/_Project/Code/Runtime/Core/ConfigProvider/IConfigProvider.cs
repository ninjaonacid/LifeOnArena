using System.Collections.Generic;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Audio;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.ConfigData.ScreenUI;
using Code.Runtime.ConfigData.Settings;
using Code.Runtime.ConfigData.StateMachine;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.UI;

namespace Code.Runtime.Core.ConfigProvider
{
    public interface IConfigProvider
    {
        EnemyDataConfig Monster(int monsterId);
        void Load();
        LevelReward Reward(LocationReward rewardId);
        StatDatabase CharacterStats();
        List<LevelReward> LoadRewards();
        VisualEffectData Particle(int id);
        LevelConfig Level(string sceneKey);
        ScreenConfig ForWindow(ScreenID menuId);
        AbilityTemplateBase Ability(int heroAbilityId);
        WeaponData Weapon(int weaponId);
        List<LevelConfig> LoadLevels();

        AudioLibrary AudioLibrary();
        AudioServiceSettings AudioServiceSettings();
        List<AbilityTemplateBase> AllAbilities();
        WeaponStateMachineDatabase GetWeaponFsmDatabase();
    }
}