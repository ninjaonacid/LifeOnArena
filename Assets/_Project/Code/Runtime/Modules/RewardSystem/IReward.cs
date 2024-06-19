using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Logic.TreasureChest;
using UnityEngine;

namespace Code.Runtime.Modules.RewardSystem
{
    public interface IReward
    {
        public bool IsOneTimeReward { get; set; }
        public RewardIdentifier RewardIdentifier { get; set; }
        public LootView LootView { get; set; }
        public void Claim();
    }
}
