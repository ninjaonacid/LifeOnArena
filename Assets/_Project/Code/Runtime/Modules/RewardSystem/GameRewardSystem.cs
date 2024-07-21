using System.Collections.Generic;
using Code.Runtime.ConfigData.Reward;
using Code.Runtime.Core.Config;
using Code.Runtime.Logic;
using Code.Runtime.Logic.TreasureChest;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.Services;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Modules.RewardSystem
{
    public class GameRewardSystem
    {
        private readonly ConfigProvider _configs;
        private readonly IGameDataContainer _gameData;
        private readonly IObjectResolver _objectResolver;
        private readonly ScreenService _screenService;

        public GameRewardSystem(ConfigProvider configs, IGameDataContainer gameData, IObjectResolver objectResolver, ScreenService screenService)
        {
            _configs = configs;
            _gameData = gameData;
            _objectResolver = objectResolver;
            _screenService = screenService;
        }

        public IReward CreateReward(int rewardId)
        {
            return null;
        }

        public TreasureChest CreateTreasureWithReward(int rewardId, TreasureChest treasurePrefab)
        {
            var rewardConfig = _configs.Reward(rewardId);
            rewardConfig.InjectServices(_gameData.PlayerData, _screenService);
            _objectResolver.Instantiate(rewardConfig);
            IReward reward = rewardConfig.GetReward();
            reward.RewardIdentifier = rewardConfig.RewardId;
            reward.IsOneTimeReward = rewardConfig.IsOneTimeReward;
            TreasureChest chest = _objectResolver.Instantiate(treasurePrefab);
            chest.SetReward(reward);
            return chest;
        }

        public TreasureChest CreateTreasureWithReward(List<RewardBlueprintBase> possibleRewards,
            TreasureChest chestPrefab)
        {
            
            IReward reward;
            
            foreach (var rewardBlueprint in possibleRewards)
            {
                if (rewardBlueprint.IsOneTimeReward)
                {
                    if(rewardBlueprint.ClaimRequirement.CheckRequirement(_gameData.PlayerData))
                    {
                        rewardBlueprint.InjectServices(_gameData.PlayerData, _screenService);
                        reward = rewardBlueprint.GetReward();
                        reward.RewardIdentifier = rewardBlueprint.RewardId;
                        reward.IsOneTimeReward = rewardBlueprint.IsOneTimeReward;
                        break;
                    }
                } 
            }

            return null;
            
        }

        public TreasureChest CreateTreasureWithReward(RewardBlueprintBase mainReward,
            RewardBlueprintBase secondReward, TreasureChest chestPrefab)
        {
            IReward reward; 

            if (mainReward.ClaimRequirement.CheckRequirement(_gameData.PlayerData))
            {
                mainReward.InjectServices(_gameData.PlayerData, _screenService);
                reward = mainReward.GetReward();
                reward.RewardIdentifier = mainReward.RewardId;
                reward.IsOneTimeReward = mainReward.IsOneTimeReward;
                reward.LootView = mainReward.RewardLootView;
            }
            else
            {
                secondReward.InjectServices(_gameData.PlayerData, _screenService);
                reward = secondReward.GetReward();
                reward.RewardIdentifier = secondReward.RewardId;
                reward.IsOneTimeReward = secondReward.IsOneTimeReward;
                reward.LootView = secondReward.RewardLootView;
            }

            TreasureChest chest = _objectResolver.Instantiate(chestPrefab);
            chest.SetReward(reward);
            return chest;
        }
    }
}