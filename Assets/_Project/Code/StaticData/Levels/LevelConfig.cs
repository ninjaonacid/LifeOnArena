using System.Collections.Generic;
using Code.StaticData.Spawners;
using UnityEngine;

namespace Code.StaticData.Levels
{
    public enum LocationType
    {
        Shelter,
        StoneDungeon,
        Forest
    }

    public enum LocationReward
    {
        Heal,
        Souls,
        None
    }

    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelConfig : ScriptableObject
    {
        public string LevelKey;
        public LocationType LocationType;

        public List<EnemySpawnerData> EnemySpawners;
        public List<WeaponPlatformSpawnerData> WeaponPlatformSpawners;
        public List<NextLevelDoorSpawnerData> NextLevelDoorSpawners;

        public Vector3 HeroInitialPosition;
        public Vector3 NextLevelDoorPosition;
        public Quaternion NextLevelDoorRotation;

        
    }
}