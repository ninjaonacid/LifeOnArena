using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Factory;
using Code.Logic.EnemySpawners;
using Code.Services;
using Code.StaticData.Levels;
using Code.StaticData.Spawners;
using Cysharp.Threading.Tasks;

namespace Code.Logic.WaveLogic
{
    public class SpawnController : IService
    {
        private readonly IStaticDataService _staticData;
        private readonly ILevelEventHandler _levelEvent;
        private readonly IEnemyFactory _enemyFactory;
        private List<EnemySpawnPoint> _enemySpawnPoints = new List<EnemySpawnPoint>();
        private int _waveCounter = 0;

        private Timer _timer;
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
            }
        }

        public void SpawnTimer()
        {
            _timer = new Timer();
        }

        public async UniTask WaveSpawn()
        {
            while (true)
            {
      
                if (IsSpawnTime() && SpawnersClear())
                {
                    foreach (EnemySpawnPoint spawner in _enemySpawnPoints)
                    {
                        spawner.Spawn();
                    }
                    _waveCounter++;
                    _timer.Reset();
                }
                
                await UniTask.Yield();

                if (SpawnersClear())
                {
                    _timer.Reset();
                }
            }
        }

        private bool IsSpawnTime()
        {
            return _timer.Elapsed > 5f;
        }

        private bool SpawnersClear()
        {
            return _enemySpawnPoints.All(x => !x.Alive);
        }
    }
}
