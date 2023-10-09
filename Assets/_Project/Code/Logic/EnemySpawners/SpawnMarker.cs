using Code.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Logic.EnemySpawners
{
    public class SpawnMarker : MonoBehaviour
    {
        public int RespawnCount;
        public MobIdentifier MobId;
    }
}