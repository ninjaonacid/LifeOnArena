using Code.StaticData;
using Code.StaticData.Ability;
using Code.StaticData.UIWindows;
using Code.UI;

namespace Code.Services
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void Load();
        ParticlesStaticData ForParticle(ParticleId id);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(UIWindowID menuId);
        HeroAbilityData ForAbility(AbilityId abilityId);
    }
}