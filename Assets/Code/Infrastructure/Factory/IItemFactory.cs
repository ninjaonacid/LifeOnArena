using Code.Logic.LevelObjectsSpawners;
using Code.Logic.ShelterWeapons;
using Code.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IItemFactory : IService
    { 
        WeaponData LoadWeapon(WeaponId weaponId);

        WeaponPlatformSpawner CreateWeaponPlatformSpawner(Vector3 point,
            string spawnerId,
            WeaponId weaponId);

        WeaponPlatform CreateWeaponPlatform(WeaponId weaponId, Transform parent);
    }
}
