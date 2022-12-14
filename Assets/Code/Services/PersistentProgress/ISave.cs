using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface ISave : ISaveReader
    {
        void UpdateProgress(PlayerProgress progress);
    }

    public interface ISaveReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}