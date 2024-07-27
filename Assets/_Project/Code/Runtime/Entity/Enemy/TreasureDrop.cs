using System;
using System.Threading;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.Reward;
using Code.Runtime.Logic.TreasureChest;
using Code.Runtime.Modules.RewardSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Enemy
{
    public class TreasureDrop : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        [SerializeField] private RewardBlueprintBase _mainReward;
        [SerializeField] private RewardBlueprintBase _secondReward;
        [SerializeField] private TreasureChest _treasurePrefab;
        [SerializeField] private AnimationDataContainer _animationData;
        
        private GameRewardSystem _gameReward;
     
        private readonly CancellationTokenSource _cts = new();
        
        [Inject]
        public void Construct(GameRewardSystem gameReward)
        {
            _gameReward = gameReward;
            _enemyDeath.Happened += SpawnTreasure;
        }

        private void SpawnTreasure()
        {
            SpawnTreasureTask().Forget();
        }

        private async UniTask SpawnTreasureTask()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_animationData.Animations[AnimationKey.Die].Length));
            var treasureChest = _gameReward.CreateTreasureWithReward(_mainReward, _secondReward, _treasurePrefab);
            treasureChest.transform.position = transform.position;
            treasureChest.transform.rotation = Quaternion.LookRotation(transform.forward);
            ActivateTreasureTask(treasureChest, _cts.Token).Forget();
        }

        private async UniTask ActivateTreasureTask(TreasureChest chest, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
            chest.Open();
        }

        private void OnDestroy()
        {
            _enemyDeath.Happened -= SpawnTreasure;
        }
    }
}
