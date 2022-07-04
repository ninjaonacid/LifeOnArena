using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.RandomService;
using CodeBase.Services.SaveLoad;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public GameFactory(IAssets assets, IPersistentProgressService progressService,
                            ISaveLoadService saveLoadService)
        {
            _assets = assets;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public GameObject CreateInventoryDisplay()
        {
            GameObject InventoryDisplay =
                InstantiateRegistered(AssetPath.InventoryDisplayPath);

            return InventoryDisplay;
        }

        public GameObject CreateInventorySlot(Transform parent)
        {
            GameObject InventorySlot = 
                InstantiateRegistered(AssetPath.InventorySlotPath, parent);
            return InventorySlot;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HudPath);

            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.Progress.WorldData);

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