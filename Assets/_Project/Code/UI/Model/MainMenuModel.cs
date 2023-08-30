using Code.Data;
using UniRx;

namespace Code.UI.Model
{
    public class MainMenuModel : IScreenModel
    {
        private readonly StatsData _playerStats;
        public ReactiveProperty<int> Health { get; } = new(); 
        public ReactiveProperty<int> Attack { get; } = new();
        public ReactiveProperty<int> Defense { get; } = new();

        public MainMenuModel(StatsData playerStats)
        {
            _playerStats = playerStats;
        }
        
        public void Initialize()
        {
            Health.Value = _playerStats.Stats["Health"];
            Attack.Value = _playerStats.Stats["Attack"];
            Defense.Value = _playerStats.Stats["Defense"];
        }
    }
}
