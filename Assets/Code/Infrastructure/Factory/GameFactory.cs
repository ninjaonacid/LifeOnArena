using Code.Infrastructure.AssetManagment;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.Services.SaveLoad;
using Code.UI.Buttons;
using Code.UI.HUD;
using Code.UI.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IWindowService _windowService;

        public GameFactory(IAssets assets, IPersistentProgressService progressService,
                            ISaveLoadService saveLoadService, IWindowService windowService)
        {
            _assets = assets;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _windowService = windowService;
        }

    

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);

            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.Progress.WorldData);

            foreach (var openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }
            return hud;
        }

        public GameObject InstantiateRegistered(string prefabPath)
        {
            var go = _assets.Instantiate(prefabPath);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }


        public GameObject InstantiateRegistered(string prefabPath, Transform parent)
        {
            var go = _assets.Instantiate(prefabPath, parent);

            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }
    }
}