using Code.Runtime.Data.PlayerData;

namespace Code.Runtime.Modules.RewardSystem
{
    public class SoulReward : IReward
    {
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
