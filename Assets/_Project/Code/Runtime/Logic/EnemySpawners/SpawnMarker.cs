using Code.Runtime.ConfigData.Identifiers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Runtime.Logic.EnemySpawners
{
    public class SpawnMarker : MonoBehaviour
    {
        public int SpawnCount;
        public MobIdentifier MobId;

        [Title("Enemy Type Button")]
        [EnumToggleButtons]
        public EnemyType EnemyType;

    }
}