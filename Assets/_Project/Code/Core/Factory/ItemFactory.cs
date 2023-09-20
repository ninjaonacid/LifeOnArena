using System.Threading.Tasks;
using Code.ConfigData;
using Code.ConfigData.Identifiers;
using Code.Core.AssetManagement;
using Code.Logic.LevelObjectsSpawners;
using Code.Logic.ShelterWeapons;
using Code.Services;
using Code.Services.ConfigData;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Core.Factory
{
    public class ItemFactory : IItemFactory
    {
        private readonly IConfigProvider _config;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAssetProvider _assetProvider;
        private readonly IGameDataContainer _gameDataContainer;

        public ItemFactory(IConfigProvider config, 
            ISaveLoadService saveLoadService, 
            IAssetProvider assetProvider,
            IGameDataContainer gameDataContainer)
        {
            _config = config;
            _saveLoadService = saveLoadService;
            _assetProvider = assetProvider;
            _gameDataContainer = gameDataContainer;
        }

        public async UniTaskVoid InitAssets()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.WeaponPlatformSpawner);
        }
        
        public WeaponData LoadWeapon(WeaponId weaponId) =>
            _config.ForWeapon(weaponId);

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
            var weaponPlatformData = _config.ForWeaponPlatforms(weaponId);

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
