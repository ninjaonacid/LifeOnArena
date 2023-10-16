using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Code.ConfigData.Levels;
using Code.ConfigData.Spawners;
using Code.Core.EntryPoints;
using Code.Core.Factory;
using Code.Logic.EnemySpawners;
using Code.Services;
using Code.Services.ConfigData;
using Code.Utils;
using Cysharp.Threading.Tasks;

namespace Code.Logic.WaveLogic
{
    public class EnemySpawnerController : IService, IDisposable
    {
        private readonly IConfigProvider _config;
        private readonly IEnemyFactory _enemyFactory;
        private readonly List<EnemySpawner> _enemySpawnPoints = new List<EnemySpawner>();
        private CancellationTokenSource _cancellationTokenSource;
        private int _waveCounter = 0;
        private readonly int _nextWaveDelay = 5;

        private Timer.Timer _timer;
        public EnemySpawnerController(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public async UniTask InitSpawners(LevelConfig levelConfig, CancellationToken token = default)
        {
            foreach (EnemySpawnerData spawnerData in levelConfig.EnemySpawners)
            {
                EnemySpawner spawner = await _enemyFactory.CreateSpawner(
                    spawnerData.Position,
                    spawnerData.Id,
                    spawnerData.MobId,
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
