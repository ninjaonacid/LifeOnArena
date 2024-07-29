using System;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services.PersistentProgress;
using TMPro;
using UniRx;

namespace Code.Runtime.UI.Model
{
    public class MainMenuModel : IScreenModel, ISavableModel
    {
        private readonly PlayerData _playerData;

        public int Level { get; private set; } = new();
        
        public ReactiveProperty<int> Souls;
        public ReactiveProperty<int> Health { get; } = new(); 
        public ReactiveProperty<int> Attack { get; } = new();
        public ReactiveProperty<int> Magic { get; } = new();

        public int StatUpgradePrice;
        

        public MainMenuModel(PlayerData playerData)
        {
            _playerData = playerData;
        }
        
        public void Initialize()
        {
            Level = _playerData.PlayerExp.Level;
            Souls = _playerData.WorldData.LootData.CollectedLoot;
         
            Health.Value = _playerData.StatsData.StatsValues["Health"];
            Attack.Value = _playerData.StatsData.StatsValues["Attack"];
            Magic.Value = _playerData.StatsData.StatsValues["Magic"];
            StatUpgradePrice = _playerData.StatsData.StatUpgradePrice;
    
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
                var statPerLevel = _playerData.StatsData.StatPerLevel[statName];
                _playerData.StatsData.StatsValues[statName] += statPerLevel;
                stat.Value += statPerLevel;
            }
        }

        private bool CanUpgradeStat(string statName, ReactiveProperty<int> stat)
        {
            return Souls.Value >= StatUpgradePrice &&
                   stat.Value < _playerData.StatsData.StatsCapacities[statName];
        }

        private void SpendLoot(int value)
        {
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
