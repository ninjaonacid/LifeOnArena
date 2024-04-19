using System.Threading.Tasks;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Logic.LevelObjectsSpawners;
using Code.Runtime.Logic.Weapon;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Core.Factory
{
    public interface IItemFactory
    { 
        WeaponData LoadWeapon(int weaponId);

        Task<WeaponPlatformSpawner> CreateWeaponPlatformSpawner(Vector3 point,
            string spawnerId,
            WeaponId weaponId);

        WeaponView CreateWeapon(WeaponView prefab, Transform position);
        
        UniTaskVoid InitAssets();
    }
}
