using Code.Data;
using Code.Data.PlayerData;

namespace Code.Services.PersistentProgress
{
    public class GameDataContainer : IGameDataContainer
    {
        public PlayerData PlayerData { get; set; }

        public AudioData AudioData { get; set; }
        
    }
}