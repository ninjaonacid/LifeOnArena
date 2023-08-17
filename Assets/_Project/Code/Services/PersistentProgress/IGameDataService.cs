using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface 
        IGameDataService : IService
    {
        PlayerData PlayerData { get; set; }
    }
}