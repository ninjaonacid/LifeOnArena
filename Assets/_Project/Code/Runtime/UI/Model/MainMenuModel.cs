using Code.Runtime.Services.PersistentProgress;
using UniRx;

namespace Code.Runtime.UI.Model
{
    public class MainMenuModel : IScreenModel
    {
        private readonly IGameDataContainer _gameData;
        
        public ReactiveProperty<int> Health { get; } = new(); 
        public ReactiveProperty<int> Attack { get; } = new();
        public ReactiveProperty<int> Defense { get; } = new();

        public bool IsMusicMuted;

        public void ChangeMusicButtonState()
        {
            IsMusicMuted = !IsMusicMuted;
        }

        public MainMenuModel(IGameDataContainer gameData)
        {
            _gameData = gameData;
        }
        
        public void Initialize()
        {
            Health.Value = _gameData.PlayerData.StatsData.Stats["Health"];
            Attack.Value = _gameData.PlayerData.StatsData.Stats["Attack"];
            Defense.Value = _gameData.PlayerData.StatsData.Stats["Defense"];
            IsMusicMuted = _gameData.AudioData.isMusicMuted;
        }
    }
}
