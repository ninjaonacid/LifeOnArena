using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface 
        IGameDataService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}