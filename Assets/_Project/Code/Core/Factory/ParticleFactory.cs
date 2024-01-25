using Code.Core.AssetManagement;
using Code.Core.ObjectPool;
using Code.Services.ConfigData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Factory
{
    public class ParticleFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly ObjectPoolProvider _poolProvider;
    
        public ParticleFactory(IAssetProvider assetProvider, ObjectPoolProvider poolProvider, IConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _objectResolver = objectResolver;
            _poolProvider = poolProvider;
        }

        public async UniTask<ParticleSystem> CreateParticle(int id)
        {
            var particle = _configProvider.Particle(id);

            var prefab = await _assetProvider.Load<GameObject>(particle.ViewReference);

            var particleSystem = _poolProvider.Pull<ParticleSystem>(id, prefab);
            
            _objectResolver.InjectGameObject(particleSystem.gameObject);
            
            return particleSystem;
        }
        
        public async UniTask<ParticleSystem> CreateParticle(int id, Vector3 position)
        {
           var particle =  await CreateParticle(id);
           particle.transform.position = position;
           return particle;
        }

        public async UniTask<ParticleSystem> CreateParticle(int id, Vector3 position, Transform parent)
        {
            var particle = await CreateParticle(id);
            Transform transform;
            (transform = particle.transform).SetParent(parent);
            transform.position = position;
            return particle;

        }
        
    }
}
