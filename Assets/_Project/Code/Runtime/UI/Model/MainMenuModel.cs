using System;
using Code.Runtime.Core.Factory;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Modules.StatSystem;
using UniRx;
using Attribute = Code.Runtime.Modules.StatSystem.Attribute;

namespace Code.Runtime.UI.Model
{
    public class MainMenuModel : IScreenModel, ISavableModel
    {
        private readonly PlayerData _playerData;
        private readonly HeroFactory _heroFactory;

        private readonly HeroStats _heroStats;
        public int Level { get; private set; } = new();
        
        public ReactiveProperty<int> Souls;
        public ReactiveProperty<int> Health { get; } = new(); 
        public ReactiveProperty<int> Attack { get; } = new();
        public ReactiveProperty<int> Magic { get; } = new();

        public int StatUpgradePrice;
        

        public MainMenuModel(PlayerData playerData, HeroFactory heroFactory)
        {
            _playerData = playerData;
            _heroFactory = heroFactory;
            _heroStats = heroFactory.HeroGameObject.GetComponent<HeroStats>();
        }
        
        public void Initialize()
        {
            Level = _playerData.PlayerExp.Level;
            Souls = _playerData.WorldData.LootData.CollectedLoot;

            Health.Value = _heroStats.Stats["Health"].Value;
            Attack.Value = _heroStats.Stats["Attack"].Value;
            Magic.Value = _heroStats.Stats["Magic"].Value;
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

                var heroStat = _heroStats.Stats[statName];
                if (heroStat is Attribute attribute)
                {
                    attribute.Add(statPerLevel);
                } 
                else if (heroStat is PrimaryStat primaryStat)
                {
                    primaryStat.Add(statPerLevel);
                }
                
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
