using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.RandomService;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _progressService;
        
        public int SpawnerCount { get; private set; }
        public GameFactory(IAssets assets, IStaticDataService staticData,
                            IRandomService randomService,
                            IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _progressService = progressService;
        }

        private GameObject HeroGameObject { get; set; }

        public List<ISavedProgressReader> ProgressReaders { get; } =
            new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
        {
            var monsterData = _staticData.ForMonster(monsterTypeId);
            var monster = Object.Instantiate(monsterData.Prefab,
                parent.position,
                Quaternion.identity, parent);

            var health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            var lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

            var attack = monster.GetComponent<EnemyAttack>();
            attack.Construct(HeroGameObject.transform);
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


        public GameObject CreateHero(GameObject initialPoint)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath,
                initialPoint.transform.position);

            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);

            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.Progress.WorldData);

            return hud;
        }

        public void CreateSpawner(Vector3 at, string spawnerDataId, MonsterTypeId spawnerDataMonsterTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.Spawner, at)
                .GetComponent<SpawnPoint>();
            spawner.Construct();
            spawner.Id = spawnerDataId;
            spawner.MonsterTypeId = spawnerDataMonsterTypeId;
            SpawnerCount++;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            var go = _assets.Instantiate(prefabPath);


            RegisterProgressWatchers(go);
            return go;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            var go = _assets.Instantiate(prefabPath, position);


            RegisterProgressWatchers(go);
            return go;
        }

        private void RegisterProgressWatchers(GameObject go)
        {
            foreach (var progressReader in
                     go.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}