using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.Weapon;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.Factory
{
    public class ItemFactory
    {
        private readonly ConfigProvider _config;
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly ObjectPoolProvider _poolProvider;

        public ItemFactory(ConfigProvider config,
            IAssetProvider assetProvider,
            IObjectResolver objectResolver,
            ObjectPoolProvider poolProvider)
        {
            _config = config;
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
        
    }
}
