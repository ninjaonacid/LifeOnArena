using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Reward;
using Code.Runtime.Core.Factory;
using Code.Runtime.Logic;
using Code.Runtime.Logic.TreasureChest;
using Code.Runtime.Modules.RewardSystem;
using Code.Runtime.Services.PersistentProgress;
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
        }
    }
}
