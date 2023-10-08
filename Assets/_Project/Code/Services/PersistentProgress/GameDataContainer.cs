using Code.Data;

namespace Code.Services.PersistentProgress
{
    public class GameDataContainer : IGameDataContainer
    {
        public PlayerData PlayerData { get; set; }

        public AudioData AudioData { get; set; }

        public GameDataContainer()
        {
            AudioData = new AudioData();
        }
    }
}