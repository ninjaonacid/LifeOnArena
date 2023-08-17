using Code.Data;

namespace Code.Services.PersistentProgress
{
    public class GameDataService : IGameDataService
    {
        public PlayerData PlayerData { get; set; }
    }
}