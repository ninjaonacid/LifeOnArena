using CodeBase.StaticData;

namespace CodeBase.Services
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void Load();
        LevelStaticData ForLevel(string sceneKey);
    }
}