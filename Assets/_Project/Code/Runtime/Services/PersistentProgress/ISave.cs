using Code.Runtime.Data.PlayerData;

namespace Code.Runtime.Services.PersistentProgress
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