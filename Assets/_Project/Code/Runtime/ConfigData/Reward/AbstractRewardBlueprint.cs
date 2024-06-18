using Code.Runtime.Modules.RewardSystem;

namespace Code.Runtime.ConfigData.Reward
{
    public abstract class AbstractRewardBlueprint<T> : RewardBlueprintBase where T : IReward
    {
        public abstract IReward GetReward();
    }
}