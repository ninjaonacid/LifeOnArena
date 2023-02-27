using System.Threading.Tasks;
using Code.Infrastructure.AssetManagment;
using Code.Logic.LevelObjectsSpawners;
using Code.Logic.ShelterWeapons;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData;
using UnityEngine;
using UnityEngine.AddressableAssets;
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

        public async Task InitAssets()
        {
            await _assetsProvider.Load<GameObject>(AssetAddress.WeaponPlatformSpawner);
        }
        public WeaponData LoadWeapon(WeaponId weaponId) =>
            _staticData.ForWeapon(weaponId);

        public async Task<WeaponPlatformSpawner> CreateWeaponPlatformSpawner(Vector3 point,
            string spawnerId,
            WeaponId weaponId)

        {
            var prefab = await _assetsProvider.Load<GameObject>(AssetAddress.WeaponPlatformSpawner);

            WeaponPlatformSpawner weaponPlatformSpawner = InstantiateRegistered(prefab, point)
                .GetComponent<WeaponPlatformSpawner>();

            weaponPlatformSpawner.Id = spawnerId;
            weaponPlatformSpawner.WeaponId = weaponId;
       
            return weaponPlatformSpawner;
        }

        public async Task<GameObject> CreateWeaponPlatform(WeaponId weaponId, Transform parent)
        {
            var weaponPlatformData = _staticData.ForWeaponPlatforms(weaponId);

            var prefab = await _assetsProvider.Load<GameObject>(weaponPlatformData.PrefabReference);

            var weaponPlatform = Object.Instantiate(prefab, parent);

            weaponPlatform.GetComponent<WeaponPlatform>().Construct(_progressService.Progress.WorldData.LootData);

            return weaponPlatform;

        }
        public GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            var go = Object.Instantiate(prefab, position, Quaternion.identity);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }
    }
}
