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
        private readonly ISaveLoadService _saveLoad;
        private readonly IGameDataContainer _gameData;
        private readonly IObjectResolver _objectResolver;
        private readonly IScreenModelFactory _modelFactory;
        private readonly SceneLoader _sceneLoader;
        
        private Transform _uiCoreTransform;

        public UIFactory(IAssetProvider assetProvider, 
            IConfigDataProvider configDataProvider, 
            ISaveLoadService saveLoad,
            IGameDataContainer gameData,
            IObjectResolver objectResolver,
            IScreenModelFactory modelFactory,
            SceneLoader sceneLoader)
        {
            _assetProvider = assetProvider;
            _configData = configDataProvider;
            _saveLoad = saveLoad;
            _gameData = gameData;
            _objectResolver = objectResolver;
            _modelFactory = modelFactory;
            _sceneLoader = sceneLoader;
        }

        public void InitAssets()
        {
            
        }

        public BaseView CreateScreenView(ScreenID screenId)
        {
            BaseView view = _configData.ForWindow(screenId).ViewPrefab;
            _objectResolver.Instantiate(view, _uiCoreTransform);
            return view;
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
