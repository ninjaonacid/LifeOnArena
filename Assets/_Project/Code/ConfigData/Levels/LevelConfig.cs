using System.Collections.Generic;
using Code.ConfigData.Spawners;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.ConfigData.Levels
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

    [CreateAssetMenu(fileName = "LevelData", menuName = "Config/Level")]
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
        [BoxGroup("Spawners")]
        public List<NextLevelDoorSpawnerData> NextLevelDoorSpawners;
        
       
        [BoxGroup("Hero")]
        public Vector3 HeroInitialPosition;
        [BoxGroup("Hero")]
        public Quaternion HeroInitialRotation;
        
        public Vector3 NextLevelDoorPosition;
        public Quaternion NextLevelDoorRotation;

        
    }
}