using System.Collections.Generic;
using Code.Infrastructure.Factory;
using Code.Logic.EnemySpawners;
using Code.Services;
using Code.StaticData.Levels;
using Code.StaticData.Spawners;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Code.Logic.WaveLogic
{
    public class SpawnController : IService
    {
        private readonly IStaticDataService _staticData;
        private readonly ILevelEventHandler _levelEvent;
        private readonly IEnemyFactory _enemyFactory;
        private List<EnemySpawnPoint> _enemySpawnPoints = new List<EnemySpawnPoint>();
        public SpawnController(ILevelEventHandler levelEventHandler, IEnemyFactory enemyFactory)
        {
            _levelEvent = levelEventHandler;
            _enemyFactory = enemyFactory;
        }

        public async UniTask InitSpawners(LevelConfig levelConfig)
        {
            foreach (EnemySpawnerData spawnerData in levelConfig.EnemySpawners)
            {
                EnemySpawnPoint spawner = await _enemyFactory.CreateSpawner(
                    spawnerData.Position,
                    spawnerData.Id,
                    spawnerData.MonsterTypeId,
                    spawnerData.RespawnCount);

                _enemySpawnPoints.Add(spawner);
                spawner.Spawn();
            }
        }
    }
}
