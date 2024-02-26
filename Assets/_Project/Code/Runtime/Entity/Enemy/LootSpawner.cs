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
        private IRandomService _randomService;
        private IItemFactory _itemFactory;
        private VisualEffectFactory _visualEffectFactory;
        private IGameDataContainer _dataContainer;
        public EnemyDeath EnemyDeath;
        [SerializeField] private ParticleIdentifier _souls;
        private int _lootMin;
        private int _lootMax;

        private CancellationTokenSource _cts;

        [Inject]
        public void Construct(IItemFactory factory, VisualEffectFactory visualEffectFactory, IRandomService randomService, IGameDataContainer dataContainer)
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
            var lootParticle = await _visualEffectFactory.CreateVisualEffect(_souls.Id);

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

            lootParticle.gameObject.SetActive(false);
        }


        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
    }
}