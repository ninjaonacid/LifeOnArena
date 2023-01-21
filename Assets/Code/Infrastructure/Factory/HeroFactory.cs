using Code.Infrastructure.AssetManagment;
using Code.Services.SaveLoad;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISaveLoadService _saveLoadService;

        public HeroFactory(IAssetsProvider assetsProvider, ISaveLoadService saveLoadService)
        {
            _assetsProvider = assetsProvider;
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
            var go = _assetsProvider.Instantiate(prefabPath, position);


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

    }
}
