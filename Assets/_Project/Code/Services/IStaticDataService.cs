using System.Collections.Generic;
using Code.StaticData;
using Code.StaticData.Ability;
using Code.StaticData.Ability.PassiveAbilities;
using Code.StaticData.Identifiers;
using Code.StaticData.Levels;
using Code.StaticData.UIWindows;
using Code.UI;

namespace Code.Services
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void Load();

        LevelReward ForReward(LocationReward rewardId);

        List<LevelReward> LoadRewards();
        ParticlesStaticData ForParticle(ParticleId id);
        LevelConfig ForLevel(string sceneKey);
        WindowConfig ForWindow(UIWindowID menuId);
        AbilityTemplateBase ForAbility(int heroAbilityId);
        WeaponData ForWeapon(WeaponId weaponId);
        WeaponPlatformStaticData ForWeaponPlatforms(WeaponId weaponId);
        List<LevelConfig> LoadLevels();
        List<PassiveAbilityTemplateBase> GetPassives();
        PassiveAbilityTemplateBase ForPassiveAbility(string abilityId);
    }
}