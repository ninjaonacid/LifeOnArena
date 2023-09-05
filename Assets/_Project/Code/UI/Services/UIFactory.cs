using System.Threading.Tasks;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.SceneManagement;
using Code.Services;
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
        private readonly IConfigDataProvider _configData;
        private readonly IObjectResolver _objectResolver;

        private Transform _uiCoreTransform;

        public UIFactory(IAssetProvider assetProvider, 
            IConfigDataProvider configDataProvider,
            IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _configData = configDataProvider;
            _objectResolver = objectResolver;
        }

        public void InitAssets()
        {
            
        }

        public BaseView CreateScreenView(ScreenID screenId)
        {
            if (!_uiCoreTransform)
            {
                CreateCore();
            }
            
            BaseView view = _configData.ForWindow(screenId).ViewPrefab;
            var viewInstance = _objectResolver.Instantiate(view, _uiCoreTransform);
            return viewInstance;
        }

        public void CreateCore()
        {
            _uiCoreTransform = _assetProvider.Instantiate(AssetAddress.UICore).transform;
        }

        public async Task<Sprite> CreateSprite(AssetReferenceSprite spriteReference)
        {
           var sprite = await _assetProvider.Load<Sprite>(spriteReference);
           return sprite;
        }
    }
}
