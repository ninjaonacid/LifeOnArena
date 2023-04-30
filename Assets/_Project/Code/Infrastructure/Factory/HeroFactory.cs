using Code.Infrastructure.AssetManagment;
using Code.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IObjectResolver _objectResolver;
        public HeroFactory(IAssetProvider assetProvider, ISaveLoadService saveLoadService, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
            _objectResolver = objectResolver;
        }
        public GameObject HeroGameObject { get; set; }

        public async UniTask InitAssets()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.Hero);
        }

        public async UniTask<GameObject> CreateHero(Vector3 initialPoint)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetAddress.Hero);

            HeroGameObject = InstantiateRegistered(prefab,
                initialPoint);

            return HeroGameObject;
        }

        public GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            
            var go = _objectResolver.Instantiate(prefab, position, Quaternion.identity);
            
            //_objectResolver.InjectGameObject(go);

            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

    }
}
