using System;
using UnityEngine;

namespace Code.StaticData.Spawners
{
    [Serializable]
    public class WeaponPlatformSpawnerData
    {
        public string Id;
        public WeaponId WeaponId;
        public Vector3 Position;
        public WeaponPlatformSpawnerData(string id, WeaponId weaponId, Vector3 position)
        {
            Id = id;
            WeaponId = weaponId;
            Position = position;
        }
    }
}
