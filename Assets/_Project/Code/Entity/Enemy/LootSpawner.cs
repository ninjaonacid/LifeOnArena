using System;
using Code.ConfigData.Identifiers;
using Code.Core.Factory;
using Code.Core.ObjectPool;
using Code.Data.PlayerData;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
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
        private ParticleSystem _lootParticle;
        private int _lootMin;
        private int _lootMax;
        
        [Inject]
        public void Construct(IItemFactory factory, IRandomService randomService, IGameDataContainer dataContainer, ParticleObjectPool particleObjectPool)
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

        private async void SpawnLootTask()
        {
            SpawnLoot().Forget();
            LootTimer().Forget();
        }
        private async UniTask SpawnLoot()
        {
            _lootParticle = await _particleObjectPool.GetObject(_souls.Id);

            _lootParticle.transform.position = transform.position + new Vector3(0, 2, 0);

            var lootItem = new Loot()
            {
                Value = _randomService.RandomizeValue(_lootMin, _lootMax)
            };
            
            _dataContainer.PlayerData.WorldData.LootData.Collect(lootItem);

        }

        private async UniTask LootTimer()
        {
            while (_lootParticle.isPlaying)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1));
            }
            
            _particleObjectPool.ReturnObject(_souls.Id, _lootParticle);
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}