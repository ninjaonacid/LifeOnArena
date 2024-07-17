using Code.Runtime.Modules.RewardSystem;
using UnityEngine;

namespace Code.Runtime.ConfigData.Reward
{
    [CreateAssetMenu(menuName = "Config/Rewards/SoulReward", fileName = "SoulReward")]
    public class SoulRewardBlueprint : AbstractRewardBlueprint<SoulReward>
    {
        [SerializeField] private int _value;
        [SerializeField] private Sprite _icon;
        public override IReward GetReward()
        {
            return new SoulReward(_screenService, _playerData, _value, _icon);
        }
    }
}