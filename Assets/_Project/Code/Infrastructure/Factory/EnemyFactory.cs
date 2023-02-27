using System.Threading.Tasks;
using Code.Enemy;
using Code.Infrastructure.AssetManagment;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.Services.SaveLoad;
using Code.StaticData;
using Code.UI.HUD;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IStaticDataService _staticData;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IProgressService _progressService;
        private readonly IRandomService _randomService;

        public EnemyFactory(IHeroFactory heroFactory, IStaticDataService staticData, IAssetsProvider assetsProvider, 
            ISaveLoadService saveLoadService, IProgressService progressService,
            IRandomService randomService)
        {
    
            _heroFactory = heroFactory;
            _staticData = staticData;
            _assetsProvider = assetsProvider;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _randomService = randomService;
        }

        public async Task InitAssets()
        {
            await _assetsProvider.Load<GameObject>(AssetAddress.EnemySpawner);
            await _assetsProvider.Load<GameObject>(AssetAddress.Loot);
        }

        public async Task<EnemySpawnPoint> CreateSpawner(Vector3 at, 
            string spawnerDataId, 
            MonsterTypeId spawnerDataMonsterTypeId, 
            int spawnerRespawnCount)
        {
            var prefab = await _assetsProvider.Load<GameObject>(AssetAddress.EnemySpawner);

            EnemySpawnPoint spawner = InstantiateRegistered(prefab, at)
                .GetComponent<EnemySpawnPoint>();

            spawner.Id = spawnerDataId;
            spawner.MonsterTypeId = spawnerDataMonsterTypeId;
            spawner.RespawnCount = spawnerRespawnCount;
            return spawner;
        }

        public async Task<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
        {
            var monsterData = _staticData.ForMonster(monsterTypeId);

            GameObject prefab = await _assetsProvider.Load<GameObject>(monsterData.PrefabReference);
            GameObject monster = Object.Instantiate<GameObject>(prefab,
                parent.position, 
                Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(_heroFactory.HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
            monster.GetComponent<EnemyTarget>().Construct(_heroFactory.HeroGameObject.transform);

            var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

            var enemyConfig = monster.GetComponent<EnemyConfig>();
      
            enemyConfig.Damage = monsterData.Damage;
            enemyConfig.Cleavage = monsterData.Cleavage;
            enemyConfig.EffectiveDistance = monsterData.EffectiveDistance;
            enemyConfig.AttackDuration = monsterData.AttackDuration;
            enemyConfig.HitStaggerDuration = monsterData.HitStaggerDuration;

            return monster;
        }


        public async Task<LootPiece> CreateLoot()
        {
            var prefab = await _assetsProvider.Load<GameObject>(AssetAddress.Loot);

            var lootPiece = InstantiateRegistered(prefab)
                .GetComponent<LootPiece>();

            lootPiece.Construct(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            var go = Object.Instantiate(prefab, position, Quaternion.identity);

            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

        public GameObject InstantiateRegistered(GameObject prefab)
        {
            var go = Object.Instantiate(prefab);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

    }
}
