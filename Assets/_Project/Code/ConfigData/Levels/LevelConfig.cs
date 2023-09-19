using System.Collections.Generic;
using Code.StaticData.Spawners;
using Sirenix.OdinInspector;
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
        [Title("Location Type")]
        [EnumToggleButtons]
        public LocationType LocationType;


        public int WavesToSpawn;
        
        [BoxGroup("Spawners")]
        [LabelWidth(100)]
        public List<EnemySpawnerData> EnemySpawners;
        public List<WeaponPlatformSpawnerData> WeaponPlatformSpawners;
        public List<NextLevelDoorSpawnerData> NextLevelDoorSpawners;

        public Vector3 HeroInitialPosition;
        public Quaternion HeroInitialRotation;
        public Vector3 NextLevelDoorPosition;
        public Quaternion NextLevelDoorRotation;

        
    }
}