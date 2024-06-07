using System.Threading.Tasks;
using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.Config;
using Code.Runtime.UI.View;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ConfigProvider _config;
        private readonly IObjectResolver _objectResolver;

        private Transform _uiCoreTransform;

        public UIFactory(IAssetProvider assetProvider, 
            ConfigProvider configProvider,
            IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _config = configProvider;
            _objectResolver = objectResolver;
        }
        
        public BaseWindowView CreateScreenView(ScreenID screenId)
        {
            if (!_uiCoreTransform)
            {
                CreateCore();
            }
            
            BaseWindowView windowView = _config.ForWindow(screenId).WindowViewPrefab;
            var viewInstance = _objectResolver.Instantiate(windowView, _uiCoreTransform);
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
