using System.Threading;
using Code.ConfigData.Identifiers;
using Code.Core.Factory;
using Code.Core.ObjectPool;
using Code.Data.PlayerData;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Entity.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        private IRandomService _randomService;
        private IItemFactory _itemFactory;
        private IGameDataContainer _dataContainer;
        private ParticleObjectPool _particleObjectPool;
        public EnemyDeath EnemyDeath;
        [SerializeField] private ParticleIdentifier _souls;
        private int _lootMin;
        private int _lootMax;

        private CancellationTokenSource _cts;

        [Inject]
        public void Construct(IItemFactory factory, IRandomService randomService, IGameDataContainer dataContainer,
            ParticleObjectPool particleObjectPool)
        {
            _itemFactory = factory;
            _randomService = randomService;
            _dataContainer = dataContainer;
            _particleObjectPool = particleObjectPool;
        }

        private void Start()
        {
            EnemyDeath.Happened += SpawnLootTask;
        }

        private void SpawnLootTask()
        {
            SpawnLoot(TaskHelper.CreateToken(ref _cts)).Forget();
        }

        private async UniTask SpawnLoot(CancellationToken token)
        {
            var lootParticle = await _particleObjectPool.GetObject(_souls.Id);

            lootParticle.transform.position = transform.position + new Vector3(0, 2, 0);

            var lootItem = new Loot()
            {
                Value = _randomService.RandomizeValue(_lootMin, _lootMax)
            };

            _dataContainer.PlayerData.WorldData.LootData.Collect(lootItem);

            
            await UniTask.WaitUntil(() =>
            {
                if (!lootParticle) _cts.Cancel();
                token.ThrowIfCancellationRequested();
                return !lootParticle.isPlaying;
            }, cancellationToken: token);

            _particleObjectPool.ReturnObject(_souls.Id, lootParticle);
        }


        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}