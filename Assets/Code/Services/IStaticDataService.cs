using Code.StaticData;

namespace Code.Services
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void Load();
        LevelStaticData ForLevel(string sceneKey);
    }
}