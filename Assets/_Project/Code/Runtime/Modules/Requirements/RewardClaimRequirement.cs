using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using UnityEngine;

namespace Code.Runtime.Modules.Requirements
{
    public class RewardClaimRequirement : IRequirement<PlayerData>
    {
        [SerializeField] private RewardIdentifier _rewardId;
        public bool CheckRequirement(PlayerData value)
        {
            return value.RewardsData.ClaimedRewards.Contains(_rewardId.Id);
        }
    }
}