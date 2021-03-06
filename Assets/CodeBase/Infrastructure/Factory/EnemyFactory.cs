using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.RandomService;
using CodeBase.Services.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IStaticDataService _staticData;
        private readonly IAssets _assets;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistentProgressService _progressService;
        private readonly IRandomService _randomService;

        public EnemyFactory(IHeroFactory heroFactory, IStaticDataService staticData, IAssets assets, 
            ISaveLoadService saveLoadService, IPersistentProgressService progressService,
            IRandomService randomService)
        {
            _heroFactory = heroFactory;
            _staticData = staticData;
            _assets = assets;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _randomService = randomService;
        }

        public void CreateSpawner(Vector3 at, string spawnerDataId, MonsterTypeId spawnerDataMonsterTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at)
                .GetComponent<SpawnPoint>();
            spawner.Construct();
            spawner.Id = spawnerDataId;
            spawner.MonsterTypeId = spawnerDataMonsterTypeId;
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
