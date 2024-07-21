using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.RandomService;
using Code.Runtime.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private VisualEffectIdentifier _souls;
        [SerializeField] private EnemyDeath _enemyDeath;

        private IRandomService _randomService;
        private VisualEffectFactory _visualEffectFactory;
        private IGameDataContainer _dataContainer;
        private LevelCollectableTracker _collectablesTracker;
        private int _lootMin;
        private int _lootMax;

        private CancellationTokenSource _cts;

        [Inject]
        public void Construct(VisualEffectFactory visualEffectFactory,
            IRandomService randomService, IGameDataContainer dataContainer, LevelCollectableTracker collectables)
        {
            _randomService = randomService;
            _visualEffectFactory = visualEffectFactory;
            _dataContainer = dataContainer;
            _collectablesTracker = collectables;
        }

        private void Start()
        {
            _enemyDeath.Happened += SpawnLootTask;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        private void SpawnLootTask()
        {
            SpawnSoulsLoot(TaskHelper.CreateToken(ref _cts)).Forget();
        }

        private async UniTask SpawnSoulsLoot(CancellationToken token)
        {
            var lootVisualEffect = await _visualEffectFactory.CreateVisualEffect(_souls.Id, cancellationToken: token);

            lootVisualEffect.transform.position = transform.position + new Vector3(0, 2, 0);

            var lootItem = new SoulLoot()
            {
                Value = _randomService.RandomizeValue(_lootMin, _lootMax)
            };
            
            _collectablesTracker.CollectSouls(lootItem.Value);
            
            _dataContainer.PlayerData.WorldData.LootData.Collect(lootItem);
        }
    }
}