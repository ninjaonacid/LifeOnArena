using System.Collections.Generic;
using Code.ConfigData;
using Code.ConfigData.Ability;
using Code.ConfigData.Identifiers;
using Code.ConfigData.Levels;
using Code.ConfigData.StatSystem;
using Code.ConfigData.UIWindows;
using Code.UI;

namespace Code.Services.ConfigData
{
    public interface IConfigProvider : IService
    {
        EnemyDataConfig ForMonster(MobId typeId);
        void Load();
        LevelReward ForReward(LocationReward rewardId);
        StatDatabase ForCharacterStats();
        List<LevelReward> LoadRewards();
        ParticlesStaticData ForParticle(ParticleId id);
        LevelConfig ForLevel(string sceneKey);
        ScreenConfig ForWindow(ScreenID menuId);
        AbilityTemplateBase ForAbility(int heroAbilityId);
        WeaponData ForWeapon(WeaponId weaponId);
        WeaponPlatformStaticData ForWeaponPlatforms(WeaponId weaponId);
        List<LevelConfig> LoadLevels();
        
    }
}