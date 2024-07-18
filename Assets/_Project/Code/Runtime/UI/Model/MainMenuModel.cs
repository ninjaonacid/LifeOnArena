using Code.Runtime.Services.PersistentProgress;
using TMPro;
using UniRx;

namespace Code.Runtime.UI.Model
{
    public class MainMenuModel : IScreenModel
    {
        private readonly IGameDataContainer _gameData;

        public bool IsTutorialEnabled { get; set; } = new();
        
        public ReactiveProperty<int> Health { get; } = new(); 
        public ReactiveProperty<int> Attack { get; } = new();
        public ReactiveProperty<int> Magic { get; } = new();

        public int StatUpgradePrice;

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
            Health.Value = _gameData.PlayerData.StatsData.StatsValues["Health"];
            Attack.Value = _gameData.PlayerData.StatsData.StatsValues["Attack"];
            Magic.Value = _gameData.PlayerData.StatsData.StatsValues["Magic"];
            StatUpgradePrice = _gameData.PlayerData.StatsData.StatUpgradePrice;
            IsMusicMuted = _gameData.AudioData.isMusicMuted;
        }

        public bool CanUpgradeHealth()
        {
            if (_gameData.PlayerData.WorldData.LootData.Collected >= StatUpgradePrice &&
                Health.Value < _gameData.PlayerData.StatsData.StatsCapacities["Health"])
            {
                return true;
            }

            return false;
        }

        public bool CanUpgradeAttack()
        {
            if (_gameData.PlayerData.WorldData.LootData.Collected >= StatUpgradePrice &&
                Health.Value < _gameData.PlayerData.StatsData.StatsCapacities["Attack"])
            {
                return true;
            }

            return false;

        }
        public bool CanUpgradeMagic()
        {
            if (_gameData.PlayerData.WorldData.LootData.Collected >= StatUpgradePrice &&
                Health.Value < _gameData.PlayerData.StatsData.StatsCapacities["Magic"])
            {
                return true;
            }

            return false;
        }
    }
}
