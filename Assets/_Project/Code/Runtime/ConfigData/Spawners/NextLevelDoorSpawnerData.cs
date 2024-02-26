using System;
using UnityEngine;

namespace Code.Runtime.ConfigData.Spawners
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
