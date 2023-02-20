using Code.Infrastructure.AssetManagment;
using Code.Logic.LevelObjectsSpawners;
using Code.Logic.ShelterWeapons;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Infrastructure.Factory
{
    public class ItemFactory : IItemFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IProgressService _progressService;

        public ItemFactory(IStaticDataService staticData, 
            ISaveLoadService saveLoadService, 
            IAssetsProvider assetsProvider,
            IProgressService progressService)
        {
            _staticData = staticData;
            _saveLoadService = saveLoadService;
            _assetsProvider = assetsProvider;
            _progressService = progressService;
        }
        public WeaponData LoadWeapon(WeaponId weaponId) =>
            _staticData.ForWeapon(weaponId);

        public WeaponPlatformSpawner CreateWeaponPlatformSpawner(Vector3 point,
            string spawnerId,
            WeaponId weaponId)

        {
            var WeaponPlatformSpawner = InstantiateRegistered(AssetAddress.WeaponPlatform, point)
                .GetComponent<WeaponPlatformSpawner>();

            WeaponPlatformSpawner.Id = spawnerId;
            WeaponPlatformSpawner.WeaponId = weaponId;
            return WeaponPlatformSpawner;
        }

        public WeaponPlatform CreateWeaponPlatform(WeaponId weaponId, Transform parent)
        {
            var weaponPlatformData = _staticData.ForWeaponPlatforms(weaponId);
            var weaponPlatform = Object.Instantiate(weaponPlatformData.Prefab, parent);
            weaponPlatform.Construct(_progressService.Progress.WorldData.LootData);
            return weaponPlatform;

        }
        public GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            var go = _assetsProvider.Instantiate(prefabPath, position);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }
    }
}
