using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Logic.TreasureChest;
using UnityEngine;

namespace Code.Runtime.Modules.RewardSystem
{
    public class SoulReward : IReward
    {
        public bool IsOneTimeReward { get; set; }
        public RewardIdentifier RewardIdentifier { get; set; }
        public LootView LootView { get; set; }

        private PlayerData _playerData;
        private int _value;
        
        public SoulReward(PlayerData playerData, int value)
        {
            _playerData = playerData;
            _value = value;
        }


        public void Claim()
        {
            _playerData.WorldData.LootData.Collect(_value);
        }
    }
}
