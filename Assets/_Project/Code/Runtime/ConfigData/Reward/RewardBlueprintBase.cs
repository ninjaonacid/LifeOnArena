using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Logic.TreasureChest;
using Code.Runtime.Modules.Requirements;
using Code.Runtime.Modules.RewardSystem;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Runtime.ConfigData.Reward
{
    public abstract class RewardBlueprintBase : SerializedScriptableObject
    {
        [SerializeField] private RewardIdentifier _rewardId;
        [SerializeField] private bool _isOneTimeReward;
        [SerializeField] private IRequirement<PlayerData> _claimRequirement;
        [SerializeField] private LootView _rewardLootView;
        
        protected PlayerData _playerData;
        protected ScreenService _screenService;
        public RewardIdentifier RewardId => _rewardId;
        public bool IsOneTimeReward => _isOneTimeReward;
        public IRequirement<PlayerData> ClaimRequirement => _claimRequirement;
        public LootView RewardLootView => _rewardLootView;
        public void InjectServices(PlayerData playerData, ScreenService screenService)
        {
            _playerData = playerData;
            _screenService = screenService;
        }

        public abstract IReward GetReward();
    }
}
