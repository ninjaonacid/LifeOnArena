using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.ConfigData.Spawners;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.Enemy;
using Code.Runtime.Logic.EnemySpawners;
using Code.Runtime.Services;
using Code.Runtime.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.WaveLogic
{
    public class EnemySpawnerController : IDisposable
    {
        public event Action<int> WaveStart;
        public event Action<int> CommonEnemiesCleared;
        public event Action BossKilled;
        public event Action<GameObject, MobIdentifier> BossSpawned;
        public int TimeToNextWave { get; private set; } = 5;

        private readonly ConfigProvider _config;
        private readonly EnemyFactory _enemyFactory;
        private readonly LevelCollectableTracker _collectablesTracker;
        private readonly List<EnemySpawner> _enemySpawnPoints = new List<EnemySpawner>();
        private CancellationTokenSource _cancellationTokenSource = new();
        private int _numberOfWaves;
        private int _aliveCommonEnemies = 0;
        private int _aliveBosses = 0;
        private bool _isBossSpawned = false;
        private bool _isBossLevel;

        public EnemySpawnerController(EnemyFactory enemyFactory, LevelCollectableTracker collectablesTracker)
        {
            _enemyFactory = enemyFactory;
            _collectablesTracker = collectablesTracker;
        }

        public async UniTask Initialize(LevelConfig levelConfig, CancellationToken token = default)
        {
            _numberOfWaves = levelConfig.WavesToSpawn;
            _isBossLevel = levelConfig.IsBossLevel;
            
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

                spawner.SpawnerCleared += SpawnerCleared;
            }
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
                    WaveStart?.Invoke(TimeToNextWave);

                    await UniTask.Delay(TimeSpan.FromSeconds(TimeToNextWave), cancellationToken: token);

                    foreach (EnemySpawner spawner in _enemySpawnPoints)
                    {
                        if (spawner.EnemyType == EnemyType.Common)
                        {
                            var monster = await spawner.Spawn(token);
                            if (monster != null)
                            {
                                _aliveCommonEnemies++;
                            }
                        }
                    }

                    waveCounter++;
                }

                if (IsWaveCleared() && waveCounter == _numberOfWaves)
                {
                    CommonEnemiesCleared?.Invoke(TimeToNextWave);
                }
                
                await UniTask.Delay(200, cancellationToken: token);

                if (IsWaveCleared() && waveCounter == _numberOfWaves && _isBossLevel && !_isBossSpawned)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(TimeToNextWave), cancellationToken: token);

                    await SpawnBoss(token);
                }

                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        private async UniTask SpawnBoss(CancellationToken token)
        {
            foreach (var spawner in _enemySpawnPoints.Where(s => s.EnemyType == EnemyType.Boss))
            {
                var bossEnemy = await spawner.Spawn(token);
                bossEnemy.GetComponent<EnemyDeath>().Happened += BossKilled;
                BossSpawned?.Invoke(bossEnemy, spawner.MobId);
                _isBossSpawned = true;
                _aliveBosses++;
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private void SpawnerCleared(EnemySpawner spawner)
        {
            if (spawner.EnemyType == EnemyType.Common)
            {
                _aliveCommonEnemies--;
            }
            else
            {
                _aliveBosses--;
            }

            _collectablesTracker.CollectKill();
        }

        private bool IsWaveCleared() => _aliveCommonEnemies == 0;
        private bool IsBossKilled() => _aliveBosses == 0;
    }
}