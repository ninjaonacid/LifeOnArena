using System.Threading.Tasks;
using Code.Infrastructure.AssetManagement;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.UIWindows;
using Code.UI.Buttons;
using Code.UI.SkillsMenu;
using Code.UI.UpgradeMenu;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Code.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadService _saveLoad;
        private readonly IProgressService _progress;
        private readonly IObjectResolver _objectResolver;
        
        private Transform _uiCoreTransform;

        public UIFactory(IAssetProvider assetProvider, 
            IStaticDataService staticDataService, 
            ISaveLoadService saveLoad,
            IProgressService progress,
            IObjectResolver objectResolver
        )
        {
            _assetProvider = assetProvider;
          
            //_heroFactory = heroFactory;
            _staticData = staticDataService;
            _saveLoad = saveLoad;
            _progress = progress;
            _objectResolver = objectResolver;
        }

        public void InitAssets()
        {
            
        }

        public ScreenBase CreateSelectionMenu(IWindowService windowService)
        {
            WindowConfig config = _staticData.ForWindow(UIWindowID.SelectionMenu);
            var menu = Object.Instantiate(config.Prefab, _uiCoreTransform);

            foreach (var openButton in menu.GetComponentsInChildren<OpenWindowButton>())
            {
                openButton.Construct(windowService);
            }

            return menu;
        }

        public void CreateUpgradeMenu()
        {
            WindowConfig config = _staticData.ForWindow(UIWindowID.UpgradeMenu);
            var window = Object.Instantiate(config.Prefab, _uiCoreTransform) as UpgradeScreen;
            //window.Construct(_abilityFactory, _heroFactory, _progress);

        }

        public void CreateSkillsMenu()
        {
            WindowConfig config = _staticData.ForWindow(UIWindowID.Skills);
            var window = _objectResolver.Instantiate(config.Prefab, _uiCoreTransform) as SkillPanelMenu;
            
        }
        public void CreateWeaponWindow()
        {
            WindowConfig config = _staticData.ForWindow(UIWindowID.Weapon);
            Object.Instantiate(config.Prefab, _uiCoreTransform);
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
