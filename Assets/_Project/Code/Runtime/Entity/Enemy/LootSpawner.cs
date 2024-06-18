using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Data.PlayerData;
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
        
        private IRandomService _randomService;
        private ItemFactory _itemFactory;
        private VisualEffectFactory _visualEffectFactory;
        private IGameDataContainer _dataContainer;
        public EnemyDeath EnemyDeath;
        private int _lootMin;
        private int _lootMax;

        private CancellationTokenSource _cts;

        [Inject]
        public void Construct(ItemFactory factory, VisualEffectFactory visualEffectFactory, IRandomService randomService, IGameDataContainer dataContainer)
        {
            _itemFactory = factory;
            _randomService = randomService;
            _visualEffectFactory = visualEffectFactory;
            _dataContainer = dataContainer;
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
            var lootVisualEffect = await _visualEffectFactory.CreateVisualEffect(_souls.Id);

            lootVisualEffect.transform.position = transform.position + new Vector3(0, 2, 0);

            var lootItem = new Loot()
            {
                Value = _randomService.RandomizeValue(_lootMin, _lootMax)
            };

            _dataContainer.PlayerData.WorldData.LootData.Collect(lootItem);
            
        }

        


        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}