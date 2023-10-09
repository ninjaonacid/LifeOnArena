using System.Threading.Tasks;
using Code.ConfigData.Identifiers;
using Code.Core.AssetManagement;
using Code.Services.ConfigData;
using UnityEngine;

namespace Code.Core.Factory
{
    public class ParticleFactory : MonoBehaviour
    {
        private AssetProvider _assetProvider;
        private IConfigProvider _configProvider;
        
        public async Task<GameObject> CreateParticle(Identifier id)
        {
            var particle = _configProvider.Particle(id.Id);

            var prefab = await _assetProvider.Load<GameObject>(particle.ParticleReference);

            return prefab;
        }
    }
}
