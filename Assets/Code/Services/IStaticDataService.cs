using Code.StaticData;
using Code.StaticData.UIWindows;
using Code.UI;

namespace Code.Services
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        void Load();
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(UIWindowID menuId);
    }
}