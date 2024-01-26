using System.Threading.Tasks;
using Code.ConfigData;
using Code.ConfigData.Identifiers;
using Code.Logic.LevelObjectsSpawners;
using Code.Logic.Weapon;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Core.Factory
{
    public interface IItemFactory : IService
    { 
        WeaponData LoadWeapon(int weaponId);

        Task<WeaponPlatformSpawner> CreateWeaponPlatformSpawner(Vector3 point,
            string spawnerId,
            WeaponId weaponId);

        MeleeWeapon CreateMeleeWeapon(GameObject prefab, Transform position);
        
        UniTaskVoid InitAssets();
    }
}
