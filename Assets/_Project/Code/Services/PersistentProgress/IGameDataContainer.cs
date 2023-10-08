using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface 
        IGameDataContainer : IService
    {
        PlayerData PlayerData { get; set; }
    
    }
}