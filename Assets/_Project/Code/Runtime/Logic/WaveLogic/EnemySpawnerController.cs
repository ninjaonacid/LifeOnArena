using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.ConfigData.Spawners;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Logic.EnemySpawners;
using Code.Runtime.Utils;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Logic.WaveLogic
{
    public class EnemySpawnerController : IDisposable
    {
        public event Action<int> WaveCleared;
        public event Action<int> CommonEnemiesCleared;
        public event Action BossKilled;

        private readonly ConfigProvider _config;
        private readonly IEnemyFactory _enemyFactory;
        
        private readonly List<EnemySpawner> _enemySpawnPoints = new List<EnemySpawner>();
        private CancellationTokenSource _cancellationTokenSource;
        private readonly int _numberOfWaves = 3;
        public int TimeToNextWave { get; set; } = 5;

        private Timer.Timer _timer;
        private bool _isBossSpawned = false;

        public EnemySpawnerController(IEnemyFactory enemyFactory, PlayerControls controls, SceneLoader sceneLoader)
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
                    spawnerData.SpawnCount,
                    spawnerData.TimeToSpawn,
                    spawnerData.EnemyType, token);

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

        private async UniTask WaveSpawn(CancellationToken token)
        {
            int waveCounter = 0;

            while (true)
            {
                if (IsWaveCleared() && waveCounter < _numberOfWaves)
                {
                    WaveCleared?.Invoke(TimeToNextWave);

                    await UniTask.Delay(TimeSpan.FromSeconds(TimeToNextWave), cancellationToken: token);
                    
                    foreach (EnemySpawner spawner in _enemySpawnPoints)
                    {
                        if (spawner.EnemyType == EnemyType.Common)
                        {
                            await spawner.Spawn(token);
                        }
                    }

                    waveCounter++;
                    _timer.Reset();
                }

                await UniTask.Delay(200, cancellationToken: token);

                if (IsWaveCleared() && waveCounter == _numberOfWaves && !_isBossSpawned)
                {
                    CommonEnemiesCleared?.Invoke(TimeToNextWave);

                    await UniTask.Delay(TimeSpan.FromSeconds(TimeToNextWave), cancellationToken: token);
                    
                    foreach (var spawner in _enemySpawnPoints)
                    {
                        if (spawner.EnemyType == EnemyType.Boss)
                        {
                            spawner.Spawn(token).Forget();
                            _isBossSpawned = true;
                        }
                    }
                }

                if (_isBossSpawned)
                {
                    if (IsBossKilled())
                    {
                        BossKilled?.Invoke();
                    }
                }
                
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
        

        private bool IsWaveCleared()
        {
            return _enemySpawnPoints
                .Where(x => x.EnemyType == EnemyType.Common)
                .All(x => !x.Alive);
        }

        private bool IsBossKilled()
        {
            return _enemySpawnPoints
                .Where(x => x.EnemyType == EnemyType.Boss)
                .All(x => !x.Alive);
        }


    }
}
