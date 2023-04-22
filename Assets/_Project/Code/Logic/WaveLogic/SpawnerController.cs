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
using UnityEngine;

namespace Code.Logic.WaveLogic
{
    public class SpawnerController : IService, IDisposable
    {
        private readonly IStaticDataService _staticData;
        private readonly ILevelEventHandler _levelEvent;
        private readonly IEnemyFactory _enemyFactory;
        private CancellationTokenSource _cancellationTokenSource;
        private List<EnemySpawner> _enemySpawnPoints = new List<EnemySpawner>();
        private int _waveCounter = 0;

        private Timer _timer;
        public SpawnerController(ILevelEventHandler levelEventHandler, IEnemyFactory enemyFactory)
        {
            _levelEvent = levelEventHandler;
            _enemyFactory = enemyFactory;
        }

        public async UniTask InitSpawners(LevelConfig levelConfig)
        {
            foreach (EnemySpawnerData spawnerData in levelConfig.EnemySpawners)
            {
                EnemySpawner spawner = await _enemyFactory.CreateSpawner(
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

        public void RunSpawner()
        {
#pragma warning disable CS4014
            WaveSpawn(TaskHelper.CreateToken(ref _cancellationTokenSource));
#pragma warning restore CS4014
        }

        public async UniTaskVoid WaveSpawn(CancellationToken token)
        {
            while (true)
            {
                if (IsSpawnTime() && SpawnersClear())
                {
                    foreach (EnemySpawner spawner in _enemySpawnPoints)
                    {
                        spawner.Spawn(token);
                    }
                    _waveCounter++;
                    _timer.Reset();
                }

                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
        public void Dispose()
        {
           _cancellationTokenSource.Cancel();
           _cancellationTokenSource.Dispose();
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
