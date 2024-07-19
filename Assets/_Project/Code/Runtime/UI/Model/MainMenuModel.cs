using System;
using Code.Runtime.Services.PersistentProgress;
using TMPro;
using UniRx;

namespace Code.Runtime.UI.Model
{
    public class MainMenuModel : IScreenModel, ISavableModel
    {
        private readonly IGameDataContainer _gameData;

        public bool IsTutorialEnabled { get; set; } = new();

        public int Level { get; private set; } = new();
        
        public ReactiveProperty<int> Souls;
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
            Level = _gameData.PlayerData.PlayerExp.Level;
            var souls = _gameData.PlayerData.WorldData.LootData.Collected;
            Souls = new ReactiveProperty<int>(souls);
            Health.Value = _gameData.PlayerData.StatsData.StatsValues["Health"];
            Attack.Value = _gameData.PlayerData.StatsData.StatsValues["Attack"];
            Magic.Value = _gameData.PlayerData.StatsData.StatsValues["Magic"];
            StatUpgradePrice = _gameData.PlayerData.StatsData.StatUpgradePrice;
            IsMusicMuted = _gameData.AudioData.isMusicMuted;
        }
        
        
        public void UpgradeHealth() => UpgradeStat("Health", Health, CanUpgradeHealth);
        public void UpgradeAttack() => UpgradeStat("Attack", Attack, CanUpgradeAttack);
        public void UpgradeMagic() => UpgradeStat("Magic", Magic, CanUpgradeMagic);

        public bool CanUpgradeHealth() => CanUpgradeStat("Health", Health);
        public bool CanUpgradeAttack() => CanUpgradeStat("Attack", Attack);
        public bool CanUpgradeMagic() => CanUpgradeStat("Magic", Magic);

        private void UpgradeStat(string statName, ReactiveProperty<int> stat, Func<bool> canUpgrade)
        {
            if (canUpgrade())
            {
                SpendLoot(StatUpgradePrice);
                var statPerLevel = _gameData.PlayerData.StatsData.StatPerLevel[statName];
                _gameData.PlayerData.StatsData.StatsValues[statName] += statPerLevel;
                stat.Value += statPerLevel;
            }
        }

        private bool CanUpgradeStat(string statName, ReactiveProperty<int> stat)
        {
            return _gameData.PlayerData.WorldData.LootData.Collected >= StatUpgradePrice &&
                   stat.Value < _gameData.PlayerData.StatsData.StatsCapacities[statName];
        }

        private void SpendLoot(int value)
        {
            _gameData.PlayerData.WorldData.LootData.Collected -= value;
            Souls.Value -= value;
        }

        public void LoadData()
        {
            
        }

        public void SaveModelData()
        {
            
        }
    }
}
