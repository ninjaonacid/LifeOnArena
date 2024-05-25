﻿using System.Collections.Generic;
using Code.Runtime.ConfigData.Spawners;
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
    public class LevelConfig : ScriptableObject
    {
        public string LevelKey;
        [Title("Location Type")]
        [EnumToggleButtons]
        public LevelType LevelType;

        public int RequiredLevel;

        public int WavesToSpawn;

        public Sprite Icon;
    
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