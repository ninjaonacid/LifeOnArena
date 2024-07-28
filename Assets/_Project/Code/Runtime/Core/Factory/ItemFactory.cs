using System.Threading.Tasks;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic;
using Code.Runtime.Logic.LevelObjectsSpawners;
using Code.Runtime.Logic.TreasureChest;
using Code.Runtime.Logic.Weapon;
using Code.Runtime.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;
using Vector3 = UnityEngine.Vector3;

namespace Code.Runtime.Core.Factory
{
    public class ItemFactory
    {
        private readonly ConfigProvider _config;
        private readonly SaveLoadService _saveLoadService;
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly ObjectPoolProvider _poolProvider;

        public ItemFactory(ConfigProvider config, 
            SaveLoadService saveLoadService, 
            IAssetProvider assetProvider,
            IObjectResolver objectResolver,
            ObjectPoolProvider poolProvider)
        {
            _config = config;
            _saveLoadService = saveLoadService;
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
            _poolProvider = poolProvider;
        }
        

        public WeaponData LoadWeapon(int weaponId)
        {
            WeaponData weaponData = _config.Weapon(weaponId);
            
            WarmAssets(weaponData.HitVisualEffect.Identifier.Id, weaponData.HitVisualEffect.PrefabReference).Forget();
            
            return weaponData;
        }

        private async UniTaskVoid WarmAssets(int id, AssetReference assetReference)
        {
            var hitVfx = await _assetProvider.Load<GameObject>(assetReference);
            _poolProvider.WarmPool(hitVfx, 5);
        }
        
        public WeaponView CreateWeapon(WeaponView prefab, Transform parent, bool worldPositionStays = true)
        {
            var weapon = _objectResolver.Instantiate(prefab, parent, worldPositionStays);
            return weapon;
        }

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

        public TreasureChest CreateTreasureChest()
        {
            return null;
        }
        
        
        // public async UniTask<GameObject> CreateLootSpawner()
        // {
        //     var prefab = await _assetProvider.Load<GameObject>(AssetAddress.Soul);
        //
        //     var lootPiece = InstantiateRegistered(prefab)
        //         .GetComponent<SoulLoot>();
        //
        //     lootPiece.Construct(_gameDataContainer.PlayerData, _heroFactory.HeroGameObject.transform);
        //     
        //     return lootPiece;
        // }
        //
        // public async Task<GameObject> CreateWeaponPlatform(WeaponId weaponId, Transform parent)
        // {
        //     var weaponPlatformData = _config.WeaponPlatforms(weaponId);
        //
        //     var prefab = await _assetProvider.Load<GameObject>(weaponPlatformData.PrefabReference);
        //
        //     var weaponPlatform = Object.Instantiate(prefab, parent);
        //
        //     weaponPlatform.GetComponent<WeaponPlatform>().Construct(_gameDataContainer.PlayerData.WorldData.LootData);
        //
        //     return weaponPlatform;
        //
        // }
        public GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            var go = Object.Instantiate(prefab, position, Quaternion.identity);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }
    }
}
