using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.ConfigData.Reward
{
    public abstract class RewardBlueprintBase : ScriptableObject
    {
        [SerializeField] private RewardIdentifier _rewardId;
        protected PlayerData _playerData;

        public RewardIdentifier RewardId => _rewardId;
        
        public void InjectServices(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}
