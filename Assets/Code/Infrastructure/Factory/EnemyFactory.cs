using Code.Enemy;
using Code.Infrastructure.AssetManagment;
using Code.Infrastructure.ObjectPool;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.Services.SaveLoad;
using Code.StaticData;
using Code.UI;
using Code.UI.HUD;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Code.Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IStaticDataService _staticData;
        private readonly IAssets _assets;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IProgressService _progressService;
        private readonly IRandomService _randomService;

        public EnemyFactory(IHeroFactory heroFactory, IStaticDataService staticData, IAssets assets, 
            ISaveLoadService saveLoadService, IProgressService progressService,
            IRandomService randomService)
        {
    
            _heroFactory = heroFactory;
            _staticData = staticData;
            _assets = assets;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _randomService = randomService;
        }

        public SpawnPoint CreateSpawner(Vector3 at, 
            string spawnerDataId, 
            MonsterTypeId spawnerDataMonsterTypeId, 
            int spawnerRespawnCount)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at)
                .GetComponent<SpawnPoint>();
            spawner.Id = spawnerDataId;
            spawner.MonsterTypeId = spawnerDataMonsterTypeId;
            spawner.RespawnCount = spawnerRespawnCount;
            return spawner;
        }

        public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
        {
            var monsterData = _staticData.ForMonster(monsterTypeId);
            var monster = Object.Instantiate<GameObject>(monsterData.Prefab,
                parent.position,
                Quaternion.identity, parent);

            var health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(_heroFactory.HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

            var attack = monster.GetComponent<EnemyAttack>();
            attack.Construct(_heroFactory.HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            return monster;
        }


        public LootPiece CreateLoot()
        {
            var lootPiece = InstantiateRegistered(AssetPath.LootPath)
                .GetComponent<LootPiece>();

            lootPiece.Construct(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            var go = _assets.Instantiate(prefabPath, position);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

        public GameObject InstantiateRegistered(string prefabPath)
        {
            var go = _assets.Instantiate(prefabPath);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }
    }
}
