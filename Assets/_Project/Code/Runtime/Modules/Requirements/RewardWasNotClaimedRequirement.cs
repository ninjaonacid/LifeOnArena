using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using UnityEngine;

namespace Code.Runtime.Modules.Requirements
{
    public class RewardWasNotClaimedRequirement : IRequirement<PlayerData>
    {
        [SerializeField] private RewardIdentifier _rewardIdentifier;
        public bool CheckRequirement(PlayerData value)
        {
           return !value.RewardsData.ClaimedRewards.Contains(_rewardIdentifier.Id);
        }
    }
}
