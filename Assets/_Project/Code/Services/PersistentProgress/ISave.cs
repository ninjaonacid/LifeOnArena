using Code.Data;
using Code.Data.PlayerData;

namespace Code.Services.PersistentProgress
{
    public interface ISave : ISaveReader
    {
        void UpdateData(PlayerData data);
    }

    public interface ISaveReader
    {
        void LoadData(PlayerData data);
    }
}