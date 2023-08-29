using System.Threading.Tasks;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.SceneManagement;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.UIWindows;
using Code.UI.Buttons;
using Code.UI.MainMenu;
using Code.UI.SkillsMenu;
using Code.UI.UpgradeMenu;
using Code.UI.View;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;
using MainMenuScreen = Code.UI.MainMenu.MainMenuScreen;

namespace Code.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigDataProvider _configData;
        private readonly ISaveLoadService _saveLoad;
        private readonly IGameDataService _gameData;
        private readonly IObjectResolver _objectResolver;
        private readonly SceneLoader _sceneLoader;
        
        private Transform _uiCoreTransform;

        public UIFactory(IAssetProvider assetProvider, 
            IConfigDataProvider configDataProvider, 
            ISaveLoadService saveLoad,
            IGameDataService gameData,
            IObjectResolver objectResolver,
            SceneLoader sceneLoader)
        {
            _assetProvider = assetProvider;
            _configData = configDataProvider;
            _saveLoad = saveLoad;
            _gameData = gameData;
            _objectResolver = objectResolver;
            _sceneLoader = sceneLoader;
        }

        public void InitAssets()
        {
            
        }


        public IScreenView CreateScreenView(ScreenID screenId)
        {
            BaseView view = _configData.ForWindow(screenId).ViewPrefab;
            _objectResolver.Instantiate(view, _uiCoreTransform);
            view.Hide();
            return view;
        }
        
        
        // public ScreenBase CreateMainMenu(IScreenViewService screenViewService)
        // {
        //     WindowConfig config = _configData.ForWindow(ScreenID.SelectionMenu);
        //     var screen = Object.Instantiate(config.Prefab, _uiCoreTransform);
        //     screen.Construct(_gameData);
        //
        //     var menu = screen as MainMenuScreen;
        //     
        //     menu.Construct(_sceneLoader);
        //
        //     foreach (var openButton in menu.GetComponentsInChildren<OpenWindowButton>())
        //     {
        //         openButton.Construct(screenViewService);
        //     }
        //
        //     return menu;
        // }
        //
        // public void CreateUpgradeMenu()
        // {
        //     WindowConfig config = _configData.ForWindow(ScreenID.UpgradeMenu);
        //     var window = Object.Instantiate(config.Prefab, _uiCoreTransform) as UpgradeScreen;
        //     //window.Construct(_abilityFactory, _heroFactory, _progress);
        //
        // }
        //
        // public void CreateSkillsMenu()
        // {
        //     WindowConfig config = _configData.ForWindow(ScreenID.Skills);
        //     var window = _objectResolver.Instantiate(config.Prefab, _uiCoreTransform) as SkillPanelMenu;
        //     
        // }
        // public void CreateWeaponWindow()
        // {
        //     WindowConfig config = _configData.ForWindow(ScreenID.Weapon);
        //     Object.Instantiate(config.Prefab, _uiCoreTransform);
        // }

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
