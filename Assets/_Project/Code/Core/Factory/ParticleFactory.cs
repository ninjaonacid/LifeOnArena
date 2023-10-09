using System.Threading.Tasks;
using Code.ConfigData.Identifiers;
using Code.Core.AssetManagement;
using Code.Services.ConfigData;
using UnityEngine;

namespace Code.Core.Factory
{
    public class ParticleFactory : MonoBehaviour
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigProvider _configProvider;

        public ParticleFactory(IAssetProvider assetProvider, IConfigProvider configProvider)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
        }

        public async Task<GameObject> CreateParticle(Identifier id)
        {
            var particle = _configProvider.Particle(id.Id);

            var prefab = await _assetProvider.Load<GameObject>(particle.ParticleReference);

            return prefab;
        }
    }
}
