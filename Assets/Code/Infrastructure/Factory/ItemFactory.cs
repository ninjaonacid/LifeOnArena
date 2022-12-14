using Code.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public class ItemFactory : IItemFactory
    {
        private readonly IStaticDataService _staticData;

        public ItemFactory(IStaticDataService staticData)
        {
            _staticData = staticData;
        }
        public WeaponData LoadWeapon(WeaponId weaponId) =>
            _staticData.ForWeapon(weaponId);
        
        
    }
}
