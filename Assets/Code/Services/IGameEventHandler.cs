using System;
using System.Collections.Generic;
using Code.Logic.EnemySpawners;
using UnityEngine;
using UnityEngine.Android;

namespace Code.Services
{
    public interface IGameEventHandler : IService
    {
        event Action PlayerDead;
        event Action MonsterSpawnersCleared;
        int LevelSpawnersCount { get; set; }
        int ClearedSpawnersCount { get; set; }
        void MonsterSpawnerSlain(SpawnPoint spawner);
        void HeroDeath();

        void ResetClearedCounter();
    }
}