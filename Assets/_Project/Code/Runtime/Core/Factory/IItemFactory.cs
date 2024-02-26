using System.Threading.Tasks;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Logic.LevelObjectsSpawners;
using Code.Runtime.Logic.Weapon;
using Code.Runtime.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Core.Factory
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
