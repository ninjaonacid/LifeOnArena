using System.Threading.Tasks;
using Code.Infrastructure.AssetManagment;
using Code.Infrastructure.Factory;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.UIWindows;
using Code.UI.Buttons;
using Code.UI.UpgradeMenu;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadService _saveLoad;
        private readonly IProgressService _progress;
      

        private Transform _uiCoreTransform;
        private readonly IAbilityFactory _abilityFactory;

        public UIFactory(IAssetsProvider assetsProvider, 
            IStaticDataService staticDataService, 
            ISaveLoadService saveLoad,
            IProgressService progress,
            IAbilityFactory abilityFactory
            )
        {
            _assetsProvider = assetsProvider;
            _abilityFactory = abilityFactory;
            //_heroFactory = heroFactory;
            _staticData = staticDataService;
            _saveLoad = saveLoad;
            _progress = progress;
        }

        public void InitAssets()
        {
            
        }

        public WindowBase CreateSelectionMenu(IWindowService windowService)
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
            var window = Object.Instantiate(config.Prefab, _uiCoreTransform) as UpgradeWindow;
            //window.Construct(_abilityFactory, _heroFactory, _progress);

        }

        public void CreateSkillsMenu()
        {
            WindowConfig config = _staticData.ForWindow(UIWindowID.Skills);
            var window = Object.Instantiate(config.Prefab, _uiCoreTransform);
        }
        public void CreateWeaponWindow()
        {
            WindowConfig config = _staticData.ForWindow(UIWindowID.Weapon);
            Object.Instantiate(config.Prefab, _uiCoreTransform);
        }

        public void CreateCore()
        {
            _uiCoreTransform = _assetsProvider.Instantiate(AssetAddress.UICore).transform;
        }

        public async Task<Sprite> CreateSprite(AssetReferenceSprite spriteReference)
        {
           var sprite = await _assetsProvider.Load<Sprite>(spriteReference);
           return sprite;
        }
    }
}
