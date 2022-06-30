using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IAssets _assets;
        private readonly ISaveLoadService _saveLoadService;

        public HeroFactory(IAssets assets, ISaveLoadService saveLoadService)
        {
            _assets = assets;
            _saveLoadService = saveLoadService;
        }
        public GameObject HeroGameObject { get; set; }

        public GameObject CreateHero(Vector3 initialPoint)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath,
                initialPoint);

            return HeroGameObject;
        }

        public GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            var go = _assets.Instantiate(prefabPath, position);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

    }
}
