using System;
using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;

namespace Code.Runtime.ConfigData.Spawners
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
