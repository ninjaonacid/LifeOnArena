using Code.Infrastructure.AssetManagment;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.UIWindows;
using Code.UI.Buttons;
using Code.UI.MainMenu;
using UnityEngine;

namespace Code.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadService _saveLoad;
        private readonly IProgressService _progress;
        private Transform _uiCoreTransform;
        public UIFactory(IAssets assets, 
            IStaticDataService staticDataService, 
            ISaveLoadService saveLoad,
            IProgressService progress)
        {
            _assets = assets;
            _staticData = staticDataService;
            _saveLoad = saveLoad;
            _progress = progress;
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

        public void CreateSkillWindow()
        {
            WindowConfig config = _staticData.ForWindow(UIWindowID.Skills);
            var window = Object.Instantiate(config.Prefab, _uiCoreTransform) as SkillMenuWindow;
            window.Construct(_progress, _staticData);
        }
        public void CreateWeaponWindow()
        {
            WindowConfig config = _staticData.ForWindow(UIWindowID.Weapon);
            Object.Instantiate(config.Prefab, _uiCoreTransform);
        }

        public void CreateCore()
        {
            _uiCoreTransform = _assets.Instantiate(AssetPath.UICore).transform;
        }
    }
}
