using Code.Infrastructure.AssetManagement;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.Buttons;
using Code.UI.HUD;
using Code.UI.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IGameDataService _gameDataService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IWindowService _windowService;
        private readonly IObjectResolver _objectResolver;
        public GameFactory(IAssetProvider assetProvider, IGameDataService gameDataService,
                            ISaveLoadService saveLoadService, IWindowService windowService, IObjectResolver objectResolver )
        {
            _assetProvider = assetProvider;
            _gameDataService = gameDataService;
            _saveLoadService = saveLoadService;
            _windowService = windowService;
            _objectResolver = objectResolver;
        }

        public GameObject CreateLevelDoor(Vector3 position, Quaternion rotation)
        {
            GameObject levelDoor = _assetProvider.Instantiate(AssetAddress.DoorPath, position);
            levelDoor.transform.rotation = rotation;

            _objectResolver.InjectGameObject(levelDoor);
            return levelDoor;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetAddress.HudPath);

            hud.GetComponentInChildren<LootCounter>()
                .Construct(_gameDataService.Progress.WorldData);

            foreach (var openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(_windowService);
            }

            return hud;
        }

        public GameObject InstantiateRegistered(string prefabPath)
        {
            var go = _assetProvider.Instantiate(prefabPath);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }


        public GameObject InstantiateRegistered(string prefabPath, Transform parent)
        {
            var go = _assetProvider.Instantiate(prefabPath, parent);

            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }
    }
}