using System.Threading.Tasks;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.LevelObjectsSpawners;
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
    public class ItemFactory : IItemFactory
    {
        private readonly IConfigProvider _config;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly ObjectPoolProvider _poolProvider;

        public ItemFactory(IConfigProvider config, 
            ISaveLoadService saveLoadService, 
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

        public async UniTaskVoid InitAssets()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.WeaponPlatformSpawner);
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
            _poolProvider.WarmPool(id, hitVfx, 5);
        }
        
        public MeleeWeapon CreateMeleeWeapon(GameObject prefab, Transform position)
        {
            var weapon = _objectResolver.Instantiate(prefab, position);
            return weapon.GetComponent<MeleeWeapon>();
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
