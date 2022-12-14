using Code.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IItemFactory : IService
    { 
        WeaponData LoadWeapon(WeaponId weaponId);

    }
}
