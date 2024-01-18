using Code.Data;
using Code.Data.PlayerData;

namespace Code.Services.PersistentProgress
{
    public interface 
        IGameDataContainer : IService
    {
        PlayerData PlayerData { get; set; }
        AudioData AudioData { get; set; }
    }
}