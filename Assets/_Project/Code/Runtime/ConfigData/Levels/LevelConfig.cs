using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Spawners;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.Requirements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Runtime.ConfigData.Levels
{
    public enum LevelType
    {
        Playable = 0,
        Unplayable = 1,
    }

    public enum LocationReward
    {
        Heal,
        Souls,
        None
    }

    [CreateAssetMenu(fileName = "LevelData", menuName = "Config/Level")]
    public class LevelConfig : SerializedScriptableObject
    {
        public string LocationName;
        public string SceneKey;
        [Title("Location Type")]
        [EnumToggleButtons]
        public LevelType LevelType;

        [FoldoutGroup("Unlock requirements list", true)]
        public List<IRequirement<PlayerData>> UnlockRequirements;
        
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
        

        public LevelIdentifier LevelId;
    }
}