using System.Collections.Generic;
using Code.StaticData;
using Code.StaticData.Ability;
using Code.StaticData.HeroUpgrades;
using Code.StaticData.Levels;
using Code.StaticData.UIWindows;
using Code.UI;

namespace Code.Services
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void Load();
        ParticlesStaticData ForParticle(ParticleId id);
        LevelConfig ForLevel(string sceneKey);
        WindowConfig ForWindow(UIWindowID menuId);
        AbilityBluePrintBase ForAbility(string heroAbilityId);
        WeaponData ForWeapon(WeaponId weaponId);
        WeaponPlatformStaticData ForWeaponPlatforms(WeaponId weaponId);
        List<LevelConfig> GetAllLevels();
        List<PowerUp> GetUpgrades();
    }
}