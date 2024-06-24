using System;
using System.Threading;
using Code.Runtime.ConfigData.Reward;
using Code.Runtime.Logic.TreasureChest;
using Code.Runtime.Modules.RewardSystem;
using Code.Runtime.Services.PersistentProgress;
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
        private GameRewardSystem _gameReward;
        private IGameDataContainer _gameData;

        private CancellationTokenSource _cts = new();
        
        [Inject]
        public void Construct(GameRewardSystem gameReward, IGameDataContainer gameData)
        {
            _gameReward = gameReward;
            _enemyDeath.Happened += SpawnTreasure;
            _gameData = gameData;
        }

        private void SpawnTreasure()
        {
            var treasureChest = _gameReward.CreateTreasureWithReward(_mainReward, _secondReward, _treasurePrefab);
            treasureChest.transform.position = transform.position;
            treasureChest.transform.rotation = Quaternion.LookRotation(transform.forward);
            ActivateTreasureTask(treasureChest, _cts.Token).Forget();

        }

        private async UniTask ActivateTreasureTask(TreasureChest chest, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: token);
            chest.Open();
        }
    }
}
