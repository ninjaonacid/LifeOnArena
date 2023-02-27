using System.Threading.Tasks;
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


            _saveLoadService.RegisterProgressWatchers(go);
            return go;
        }

    }
}
