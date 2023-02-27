using System;
using UnityEngine;

namespace Code.StaticData.Spawners
{
    [Serializable]
    public class NextLevelDoorSpawnerData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public NextLevelDoorSpawnerData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}
