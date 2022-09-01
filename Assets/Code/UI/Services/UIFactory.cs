using Code.Infrastructure.AssetManagment;
using Code.Services;
using Code.StaticData.UIWindows;
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

        public void CreateSelectionMenu()
        {
            WindowConfig config = _staticData.ForWindow(UIWindowID.SelectionMenu);
            Object.Instantiate(config.Prefab, _uiCoreTransform);
        }

        public void CreateCore()
        {
            _uiCoreTransform = _assets.Instantiate(AssetPath.UICore).transform;
        }
    }
}
