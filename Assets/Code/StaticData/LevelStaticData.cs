using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;

        public List<EnemySpawnerData> EnemySpawners;

        public Vector3 HeroInitialPosition;

        public Vector3 NextLevelDoorPosition;
        public Quaternion NextLevelDoorRotation;
    }
}