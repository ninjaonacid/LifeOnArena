using System;
using System.Threading;
using Code.Entity.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.Logic.EnemySpawners;
using Code.Logic.EntitiesComponents;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.Services.SaveLoad;
using Code.StaticData.Identifiers;
using Code.StaticData.StatSystem;
using Code.UI.HUD;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory, IDisposable
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IConfigDataProvider _configData;
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameDataContainer _gameDataContainer;
        private readonly IRandomService _randomService;
        private readonly IObjectResolver _objectResolver;

        private readonly CancellationTokenSource _cancellationTokenSource = default;
        public EnemyFactory(IHeroFactory heroFactory, IConfigDataProvider configData, IAssetProvider assetProvider, 
            ISaveLoadService saveLoadService, IGameDataContainer gameDataContainer,
            IRandomService randomService, IObjectResolver objectResolver)
        {
            _heroFactory = heroFactory;
            _configData = configData;
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
            _gameDataContainer = gameDataContainer;
            _randomService = randomService;
            _objectResolver = objectResolver;
        }

        public async UniTask InitAssets()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawner);
            await _assetProvider.Load<GameObject>(AssetAddress.Loot);
        }

        public async UniTask<EnemySpawner> CreateSpawner(Vector3 at,
            string spawnerDataId,
            MonsterTypeId spawnerDataMonsterTypeId,
            int spawnerRespawnCount, CancellationToken token)
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawner);

            EnemySpawner spawner = InstantiateRegistered(prefab, at)
                .GetComponent<EnemySpawner>();

            spawner.Id = spawnerDataId;
            spawner.MonsterTypeId = spawnerDataMonsterTypeId;
            spawner.RespawnCount = spawnerRespawnCount;

            return spawner;
        }

        public async UniTask<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent, CancellationToken token)
        {
            var monsterData = _configData.ForMonster(monsterTypeId);

            GameObject prefab = await _assetProvider.Load<GameObject>(monsterData.PrefabReference);
            GameObject monster = _objectResolver.Instantiate<GameObject>(prefab,
                parent.position, 
                Quaternion.identity, parent);

            StatController monsterStats = monster.GetComponent<StatController>();
            
            IDamageable damageable = monster.GetComponent<IDamageable>();

            monster.GetComponent<ActorUI>().Construct(damageable);
            monster.GetComponent<AgentMoveToPlayer>().Construct(_heroFactory.HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
            monster.GetComponent<EnemyTarget>().Construct(_heroFactory.HeroGameObject.transform);

            var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

            var fsm = monster.GetComponent<EnemyStateMachine>();
            fsm.Construct(monsterData.EnemyStateMachineConfig);
          

            return monster;
        }


        public async UniTask<LootPiece> CreateLoot()
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.Loot);

            var lootPiece = InstantiateRegistered(prefab)
                .GetComponent<LootPiece>();

            lootPiece.Construct(_gameDataContainer.PlayerData.WorldData);
            return lootPiece;
        }

        public GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            var go = _objectResolver.Instantiate(prefab, position, Quaternion.identity);

            //_objectResolver.InjectGameObject(go);

            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

        public GameObject InstantiateRegistered(GameObject prefab)
        {
            var go = Object.Instantiate(prefab);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}
