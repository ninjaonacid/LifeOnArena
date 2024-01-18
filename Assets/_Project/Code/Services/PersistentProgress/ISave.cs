using Code.Data;
using Code.Data.PlayerData;

namespace Code.Services.PersistentProgress
{
    public interface ISave : ISaveLoader
    {
        void UpdateData(PlayerData data);
    }

    public interface ISaveLoader
    {
        void LoadData(PlayerData data);
    }
}