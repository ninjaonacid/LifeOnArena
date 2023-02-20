using Code.Infrastructure.AssetManagment;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.Buttons;
using Code.UI.HUD;
using Code.UI.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly IStaticDataService _staticData;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IWindowService _windowService;

        public GameFactory(IAssetsProvider assetsProvider, IProgressService progressService,
                            ISaveLoadService saveLoadService, IWindowService windowService)
        {
            _assetsProvider = assetsProvider;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _windowService = windowService;
        }

        public GameObject CreateLevelDoor(Vector3 position)
        {
            GameObject levelDoor = _assetsProvider.Instantiate(AssetAddress.DoorPath, position);

            return levelDoor;
        }
        public GameObject CreateLevelEventHandler()
        {
            GameObject levelHandler = _assetsProvider.Instantiate(AssetAddress.LevelEventHandler);

            return levelHandler;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetAddress.HudPath);

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
            var go = _assetsProvider.Instantiate(prefabPath);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }


        public GameObject InstantiateRegistered(string prefabPath, Transform parent)
        {
            var go = _assetsProvider.Instantiate(prefabPath, parent);

            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }
    }
}