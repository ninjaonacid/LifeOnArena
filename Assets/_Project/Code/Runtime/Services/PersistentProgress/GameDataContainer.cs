using Code.Runtime.Data;
using Code.Runtime.Data.PlayerData;

namespace Code.Runtime.Services.PersistentProgress
{
    public class GameDataContainer : IGameDataContainer
    {
        public PlayerData PlayerData { get; set; }

        public AudioData AudioData { get; set; }
        
    }
}