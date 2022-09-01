using Code.Infrastructure.AssetManagment;
using Code.Services;
using UnityEngine;

namespace Code.UI.Services
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private IStaticDataService _staticDataService;
        private Transform UICoreTransform;
        public UIFactory(IAssets _assets)
        {
            
        }

        public void CreateCore()
        {
            UICoreTransform = _assets.Instantiate(AssetPath.UICore).transform;
        }
    }
}
