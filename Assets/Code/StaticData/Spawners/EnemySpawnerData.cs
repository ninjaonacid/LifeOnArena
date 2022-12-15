using System;
using UnityEngine;

namespace Code.StaticData.Spawners
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public int RespawnCount;
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;

        public EnemySpawnerData(string id, MonsterTypeId monsterTypeId, Vector3 position, int respawnCount)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
            RespawnCount = respawnCount;
        }
    }
}