using System.Threading.Tasks;
using Code.Core.AssetManagement;
using Code.Services;
using Code.Services.ConfigData;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.View;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Code.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigProvider _config;
        private readonly IObjectResolver _objectResolver;

        private Transform _uiCoreTransform;

        public UIFactory(IAssetProvider assetProvider, 
            IConfigProvider configProvider,
            IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _config = configProvider;
            _objectResolver = objectResolver;
        }
        
        public BaseView CreateScreenView(ScreenID screenId)
        {
            if (!_uiCoreTransform)
            {
                CreateCore();
            }
            
            BaseView view = _config.ForWindow(screenId).ViewPrefab;
            var viewInstance = _objectResolver.Instantiate(view, _uiCoreTransform);
            return viewInstance;
        }

        public void CreateCore()
        {
            _uiCoreTransform = _assetProvider.InstantiateSync(AssetAddress.UICore).transform;
        }

        public async Task<Sprite> CreateSprite(AssetReferenceSprite spriteReference)
        {
           var sprite = await _assetProvider.Load<Sprite>(spriteReference);
           return sprite;
        }
    }
}
