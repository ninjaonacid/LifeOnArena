using System;
using Code.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.ConfigData.Spawners
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public int RespawnCount;
        [FormerlySerializedAs("MonsterTypeId")] public MobId MobId;
        public Vector3 Position;

        public EnemySpawnerData(string id, MobId mobId, Vector3 position, int respawnCount)
        {
            Id = id;
            MobId = mobId;
            Position = position;
            RespawnCount = respawnCount;
        }
    }
}