using System;
using Code.ConfigData.Identifiers;
using UnityEngine;

namespace Code.ConfigData.Spawners
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
