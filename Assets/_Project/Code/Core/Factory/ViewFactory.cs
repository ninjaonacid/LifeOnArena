using System.Threading.Tasks;
using Code.Core.AssetManagement;
using Code.Services.ConfigData;
using UnityEngine;

namespace Code.Core.Factory
{
    public class ViewFactory : MonoBehaviour
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigProvider _configProvider;

        public ViewFactory(IAssetProvider assetProvider, IConfigProvider configProvider)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
        }

        public async Task<GameObject> CreateVfx(int id)
        {
            var particle = _configProvider.Vfx(id);

            var prefab = await _assetProvider.Load<GameObject>(particle.ViewReference);

            return prefab;
        }
    }
}
