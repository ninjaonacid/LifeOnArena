using System.Threading.Tasks;
using Code.Infrastructure.AssetManagment;
using Code.Services.SaveLoad;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IObjectResolver _objectResolver;
        public HeroFactory(IAssetsProvider assetsProvider, ISaveLoadService saveLoadService, IObjectResolver objectResolver)
        {
            _assetsProvider = assetsProvider;
            _saveLoadService = saveLoadService;
            _objectResolver = objectResolver;
        }
        public GameObject HeroGameObject { get; set; }

        public void InitAssets()
        {
            _assetsProvider.Load<GameObject>(AssetAddress.Hero);
        }
        public async Task<GameObject> CreateHero(Vector3 initialPoint)
        {
            GameObject prefab = await _assetsProvider.Load<GameObject>(AssetAddress.Hero);

            

            HeroGameObject = InstantiateRegistered(prefab,
                initialPoint);
            return HeroGameObject;
        }

        public GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            
            var go = Object.Instantiate(prefab, position, Quaternion.identity);
            
            _objectResolver.InjectGameObject(go);
            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

    }
}
