using System;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Logic.EnemySpawners;
using UnityEngine;

namespace Code.Runtime.ConfigData.Spawners
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public int SpawnCount;
        public float TimeToSpawn;
        public MobIdentifier MobId;
        public Vector3 Position;
        public EnemyType EnemyType;

        public EnemySpawnerData(string id, MobIdentifier mobId, Vector3 position, int spawnCount, float timeToSpawn, EnemyType enemyType)
        {
            Id = id;
            MobId = mobId;
            Position = position;
            SpawnCount = spawnCount;
            TimeToSpawn = timeToSpawn;
            EnemyType = enemyType;
        }
    }
}