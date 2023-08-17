using Code.Data;

namespace Code.Services.PersistentProgress
{
    public class GameDataService : IGameDataService
    {
        public PlayerProgress Progress { get; set; }
    }
}