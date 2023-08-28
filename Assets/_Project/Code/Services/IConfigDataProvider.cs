using System.Collections.Generic;
using Code.StaticData;
using Code.StaticData.Ability;
using Code.StaticData.Identifiers;
using Code.StaticData.Levels;
using Code.StaticData.StatSystem;
using Code.StaticData.UIWindows;
using Code.UI;

namespace Code.Services
{
    public interface IConfigDataProvider : IService
    {
        EnemyDataConfig ForMonster(MonsterTypeId typeId);
        void Load();
        LevelReward ForReward(LocationReward rewardId);
        StatDatabase ForCharacterStats();
        List<LevelReward> LoadRewards();
        ParticlesStaticData ForParticle(ParticleId id);
        LevelConfig ForLevel(string sceneKey);
        WindowConfig ForWindow(ScreenID menuId);
        AbilityTemplateBase ForAbility(int heroAbilityId);
        WeaponData ForWeapon(WeaponId weaponId);
        WeaponPlatformStaticData ForWeaponPlatforms(WeaponId weaponId);
        List<LevelConfig> LoadLevels();
        
    }
}