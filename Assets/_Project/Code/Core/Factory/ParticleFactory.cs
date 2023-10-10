using System;
using System.Threading.Tasks;
using Code.Core.AssetManagement;
using Code.Services.ConfigData;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Code.Core.Factory
{
    public class ParticleFactory : MonoBehaviour
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;

        public ParticleFactory(IAssetProvider assetProvider, IConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _objectResolver = objectResolver;
        }

        public async Task<ParticleSystem> CreateParticle(int id)
        {
            var particle = _configProvider.Particle(id);

            var prefab = await _assetProvider.Load<GameObject>(particle.ViewReference);

            var particleSystem = Object.Instantiate(prefab).GetComponent<ParticleSystem>();
            
            _objectResolver.InjectGameObject(particleSystem.gameObject);
            
            return particleSystem;
        }
    }
}
