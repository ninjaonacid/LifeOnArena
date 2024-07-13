using System;
using System.Collections.Generic;
using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Entity.Enemy;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Logic.EnemySpawners;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.RandomService;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.View.HUD;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Code.Runtime.Core.Factory
{
    public class EnemyFactory : IDisposable
    {
        private readonly IHeroFactory _heroFactory;
        private readonly ConfigProvider _config;
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameDataContainer _gameDataContainer;
        private readonly IRandomService _randomService;
        private readonly IObjectResolver _objectResolver;
        private readonly LevelLoader _levelLoader;
        private readonly ObjectPoolProvider _objectPoolProvider;
        private readonly Dictionary<MobIdentifier, GameObject> _spawnedEntities;

        private readonly CancellationTokenSource _cancellationTokenSource = default;
        public EnemyFactory(IHeroFactory heroFactory, ConfigProvider config, IAssetProvider assetProvider, 
            ISaveLoadService saveLoadService, IGameDataContainer gameDataContainer,
            IRandomService randomService, IObjectResolver objectResolver, LevelLoader levelLoader, ObjectPoolProvider poolProvider)
        {
            _heroFactory = heroFactory;
            _config = config;
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
            _gameDataContainer = gameDataContainer;
            _randomService = randomService;
            _objectResolver = objectResolver;
            _levelLoader = levelLoader;
            _objectPoolProvider = poolProvider;
        }

        public async UniTask InitAssets()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawner);
            await _assetProvider.Load<GameObject>(AssetAddress.Soul);
        }

        public async UniTask<EnemySpawner> CreateSpawner(Vector3 at,
            string spawnerDataId,
            MobIdentifier spawnerDataMobId,
            int spawnerRespawnCount, float timeToSpawn, EnemyType enemyType, CancellationToken token)
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawner);

            EnemySpawner spawner = InstantiateRegistered(prefab, at)
                .GetComponent<EnemySpawner>();

            spawner.Id = spawnerDataId;
            spawner.MobId = spawnerDataMobId;
            spawner.RespawnCount = spawnerRespawnCount;
            spawner.EnemyType = enemyType;
            spawner.TimeToSpawn = timeToSpawn;
            spawner.InitializeSpawner();

            return spawner;
        }
        
        public async UniTask<GameObject> CreateMonster(int mobId, Transform parent, CancellationToken token,
            Action<PooledObject> onCreate = null, Action<PooledObject> onGet = null, 
            Action<PooledObject>  onReturn = null, Action<PooledObject> onRelease = null)
        {
            var monsterData = _config.Monster(mobId);
            var levelConfig = _levelLoader.GetCurrentLevelConfig();

            GameObject prefab = await _assetProvider.Load<GameObject>(monsterData.PrefabReference, token);
            
            GameObject monster = _objectPoolProvider.Spawn(prefab, onCreate: onCreate, onRelease: onRelease, onGet: onGet, onReturn: onReturn);
            
            monster.transform.SetParent(parent);
            
            IDamageable damageable = monster.GetComponent<IDamageable>();

            var enemyStats = monster.GetComponent<EnemyStats>();

            enemyStats.SetStats(monsterData.PossibleStats.Count > 1
                ? monsterData.PossibleStats[levelConfig.LevelDifficulty - 1]
                : monsterData.PossibleStats[0]);
            
            monster.GetComponent<EnemyDeath>().Initialize();
            monster.GetComponent<EntityUI>().Construct(damageable);
            monster.GetComponent<NavMeshMoveToPlayer>().Construct(_heroFactory.HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
            monster.GetComponent<EnemyTarget>().Construct(_heroFactory.HeroGameObject.transform);

            var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterData.MinSouls, monsterData.MaxSouls);

            var expDrop = monster.GetComponentInChildren<ExpDrop>();
            expDrop.Construct(_randomService, _gameDataContainer.PlayerData.PlayerExp);
            expDrop.SetExperienceGain(monsterData.MinExp, monsterData.MaxExp);

            return monster;
        }
        

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            var go = _objectResolver.Instantiate(prefab, position, Quaternion.identity);

            //_objectResolver.InjectGameObject(go);

            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
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
