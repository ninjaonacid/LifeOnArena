using System.Threading.Tasks;
using Code.Logic.LevelObjectsSpawners;
using Code.Services;
using Code.StaticData;
using Code.StaticData.Identifiers;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IItemFactory : IService
    { 
        WeaponData LoadWeapon(WeaponId weaponId);

        Task<WeaponPlatformSpawner> CreateWeaponPlatformSpawner(Vector3 point,
            string spawnerId,
            WeaponId weaponId);

        Task<GameObject> CreateWeaponPlatform(WeaponId weaponId, Transform parent);

        Task InitAssets();
    }
}
