using System;
using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;

namespace Code.Runtime.ConfigData.Spawners
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public int RespawnCount;
        public MobIdentifier MobId;
        public Vector3 Position;

        public EnemySpawnerData(string id, MobIdentifier mobId, Vector3 position, int respawnCount)
        {
            Id = id;
            MobId = mobId;
            Position = position;
            RespawnCount = respawnCount;
        }
    }
}