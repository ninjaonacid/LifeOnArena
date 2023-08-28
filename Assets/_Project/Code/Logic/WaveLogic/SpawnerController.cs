using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Code.Infrastructure.Factory;
using Code.Logic.EnemySpawners;
using Code.Services;
using Code.StaticData.Levels;
using Code.StaticData.Spawners;
using Code.Utils;
using Cysharp.Threading.Tasks;

namespace Code.Logic.WaveLogic
{
    public class SpawnerController : IService, IDisposable
    {
        private readonly IConfigDataProvider _configData;
        private readonly ILevelEventHandler _levelEvent;
        private readonly IEnemyFactory _enemyFactory;
        private CancellationTokenSource _cancellationTokenSource;
        private List<EnemySpawner> _enemySpawnPoints = new List<EnemySpawner>();
        private int _waveCounter = 0;
        private int _nextWaveDelay = 5;

        private Timer.Timer _timer;
        public SpawnerController(ILevelEventHandler levelEventHandler, IEnemyFactory enemyFactory)
        {
            _levelEvent = levelEventHandler;
            _enemyFactory = enemyFactory;
        }

        public async UniTask InitSpawners(LevelConfig levelConfig, CancellationToken token = default)
        {
            foreach (EnemySpawnerData spawnerData in levelConfig.EnemySpawners)
            {
                EnemySpawner spawner = await _enemyFactory.CreateSpawner(
                    spawnerData.Position,
                    spawnerData.Id,
                    spawnerData.MonsterTypeId,
                    spawnerData.RespawnCount, token);

                _enemySpawnPoints.Add(spawner);
            }
        }

        public void SpawnTimer()
        {
            _timer = new Timer.Timer();
        }

        public void RunSpawner()
        {
            WaveSpawn(TaskHelper.CreateToken(ref _cancellationTokenSource)).Forget();
        }

        private async UniTaskVoid WaveSpawn(CancellationToken token)
        {
            while (true)
            {
                if (SpawnersClear())
                {
                    await UniTask.Delay(ConvertSeconds(_nextWaveDelay), cancellationToken: token);
                    foreach (EnemySpawner spawner in _enemySpawnPoints)
                    {
                        spawner.Spawn(token).Forget();
                    }
                    _waveCounter++;
                    _timer.Reset();
                }

                await UniTask.Delay(200, cancellationToken: token);

                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        public void Dispose()
        {
           _cancellationTokenSource.Cancel();
           _cancellationTokenSource.Dispose();
        }

        private bool IsSpawnTime()
        {
            return _timer.Elapsed >= 5f;
        }

        private bool SpawnersClear()
        {
            return _enemySpawnPoints.All(x => !x.Alive);
        }

        private int ConvertSeconds(int seconds)
        {
            return seconds * 1000;
        }
    }
}
