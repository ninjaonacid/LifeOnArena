using System.Threading.Tasks;
using Code.Infrastructure.AssetManagement;
using Code.Logic.LevelObjectsSpawners;
using Code.Logic.ShelterWeapons;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData;
using Code.StaticData.Identifiers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Infrastructure.Factory
{
    public class ItemFactory : IItemFactory
    {
        private readonly IConfigDataProvider _configData;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameDataContainer _gameDataContainer;

        public ItemFactory(IConfigDataProvider configData, 
            ISaveLoadService saveLoadService, 
            IAssetProvider assetProvider,
            IGameDataContainer gameDataContainer)
        {
            _configData = configData;
            _saveLoadService = saveLoadService;
            _assetProvider = assetProvider;
            _gameDataContainer = gameDataContainer;
        }

        public async Task InitAssets()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.WeaponPlatformSpawner);
        }
        public WeaponData LoadWeapon(WeaponId weaponId) =>
            _configData.ForWeapon(weaponId);

        public async Task<WeaponPlatformSpawner> CreateWeaponPlatformSpawner(Vector3 point,
            string spawnerId,
            WeaponId weaponId)

        {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.WeaponPlatformSpawner);

            WeaponPlatformSpawner weaponPlatformSpawner = InstantiateRegistered(prefab, point)
                .GetComponent<WeaponPlatformSpawner>();

            weaponPlatformSpawner.Id = spawnerId;
            weaponPlatformSpawner.WeaponId = weaponId;
       
            return weaponPlatformSpawner;
        }

        public async Task<GameObject> CreateWeaponPlatform(WeaponId weaponId, Transform parent)
        {
            var weaponPlatformData = _configData.ForWeaponPlatforms(weaponId);

            var prefab = await _assetProvider.Load<GameObject>(weaponPlatformData.PrefabReference);

            var weaponPlatform = Object.Instantiate(prefab, parent);

            weaponPlatform.GetComponent<WeaponPlatform>().Construct(_gameDataContainer.PlayerData.WorldData.LootData);

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
