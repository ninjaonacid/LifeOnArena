using System;
using System.Threading;
using Code.ConfigData.Identifiers;
using Code.ConfigData.StatSystem;
using Code.Core.AssetManagement;
using Code.Entity.Enemy;
using Code.Logic.EnemySpawners;
using Code.Logic.EntitiesComponents;
using Code.Logic.Particles;
using Code.Services.ConfigData;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.Services.SaveLoad;
using Code.UI.HUD;
using Code.UI.View.HUD;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Code.Core.Factory
{
    public class EnemyFactory : IEnemyFactory, IDisposable
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IConfigProvider _config;
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameDataContainer _gameDataContainer;
        private readonly IRandomService _randomService;
        private readonly IObjectResolver _objectResolver;

        private readonly CancellationTokenSource _cancellationTokenSource = default;
        public EnemyFactory(IHeroFactory heroFactory, IConfigProvider config, IAssetProvider assetProvider, 
            ISaveLoadService saveLoadService, IGameDataContainer gameDataContainer,
            IRandomService randomService, IObjectResolver objectResolver)
        {
            _heroFactory = heroFactory;
            _config = config;
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
            _gameDataContainer = gameDataContainer;
            _randomService = randomService;
            _objectResolver = objectResolver;
        }

        public async UniTask InitAssets()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawner);
            await _assetProvider.Load<GameObject>(AssetAddress.Soul);
        }

        public async UniTask<EnemySpawner> CreateSpawner(Vector3 at,
            string spawnerDataId,
            MobIdentifier spawnerDataMobId,
            int spawnerRespawnCount, CancellationToken token)
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawner);

            EnemySpawner spawner = InstantiateRegistered(prefab, at)
                .GetComponent<EnemySpawner>();

            spawner.Id = spawnerDataId;
            spawner.MobId = spawnerDataMobId;
            spawner.RespawnCount = spawnerRespawnCount;

            return spawner;
        }

        public async UniTask<GameObject> CreateMonster(int mobId, Transform parent, CancellationToken token)
        {
            var monsterData = _config.Monster(mobId);

            GameObject prefab = await _assetProvider.Load<GameObject>(monsterData.PrefabReference);
            GameObject monster = _objectResolver.Instantiate(prefab,
                parent.position, 
                Quaternion.identity, parent);
            
            
            IDamageable damageable = monster.GetComponent<IDamageable>();

            monster.GetComponent<EntityUI>().Construct(damageable);
            monster.GetComponent<AgentMoveToPlayer>().Construct(_heroFactory.HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
            monster.GetComponent<EnemyTarget>().Construct(_heroFactory.HeroGameObject.transform);

            var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

            var expDrop = monster.GetComponentInChildren<ExpDrop>();
            expDrop.Construct(_randomService, _gameDataContainer.PlayerData.PlayerExp);
            expDrop.SetExperienceGain(monsterData.MinExp, monsterData.MaxExp);
            var fsm = monster.GetComponent<EnemyStateMachine>();
            fsm.Construct(monsterData.EnemyStateMachineConfig);
          

            return monster;
        }


        public async UniTask<SoulLoot> CreateSoulLoot()
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.Soul);

            var lootPiece = InstantiateRegistered(prefab)
                .GetComponent<SoulLoot>();

            lootPiece.Construct(_gameDataContainer.PlayerData, _heroFactory.HeroGameObject.transform);
            
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
