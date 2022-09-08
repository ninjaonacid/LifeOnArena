using Code.Infrastructure.AssetManagment;
using Code.Services;
using Code.StaticData.UIWindows;
using Code.UI.Buttons;
using UnityEngine;

namespace Code.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private Transform _uiCoreTransform;
        public UIFactory(IAssets assets, IStaticDataService staticDataService)
        {
            _assets = assets;
            _staticData = staticDataService;
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
            Object.Instantiate(config.Prefab, _uiCoreTransform);
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
