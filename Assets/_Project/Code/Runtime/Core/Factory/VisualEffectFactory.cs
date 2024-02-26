using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Core.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Core.Factory
{
    public class VisualEffectFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly ObjectPoolProvider _poolProvider;
    
        public VisualEffectFactory(IAssetProvider assetProvider, ObjectPoolProvider poolProvider, IConfigProvider configProvider, IObjectResolver objectResolver)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _objectResolver = objectResolver;
            _poolProvider = poolProvider;
        }

        public async UniTask<ParticleSystem> CreateVisualEffect(int id)
        {
            var particle = _configProvider.Particle(id);

            var prefab = await _assetProvider.Load<GameObject>(particle.PrefabReference);
            
            var particleSystem = _poolProvider.Spawn<ParticleSystem>(id, prefab);

            return particleSystem;
        }
        
        public async UniTask<ParticleSystem> CreateVisualEffect(int id, Vector3 position)
        {
           var particle =  await CreateVisualEffect(id);
           particle.transform.position = position;
           return particle;
        }

        public async UniTask<ParticleSystem> CreateParticleWithTimer(int id, float time)
        {
            var particle = await CreateVisualEffect(id);
            _poolProvider.ReturnWithTimer(id, particle.GetComponent<PooledObject>(), time).Forget();
            return particle;
        }

        public async UniTask<ParticleSystem> CreateParticleWithTimer(int id, Vector3 position, float time)
        {
            var particle = await CreateVisualEffect(id);
            _poolProvider.ReturnWithTimer(id, particle.GetComponent<PooledObject>(), time).Forget();
            particle.transform.position = position;
            return particle;
        }

        public async UniTask<ParticleSystem> CreateVisualEffect(int id, Vector3 position, Transform parent)
        {
            var particle = await CreateVisualEffect(id);
            Transform transform;
            (transform = particle.transform).SetParent(parent);
            transform.position = position;
            return particle;
        }

        public async UniTaskVoid PrewarmParticlePool(int id, int size)
        {
            var particle = _configProvider.Particle(id);

            var prefab = await _assetProvider.Load<GameObject>(particle.PrefabReference);

            _poolProvider.WarmPool(prefab, size);
        }

        public void PrewarmParticlePool(GameObject prefab, int size)
        {
            _poolProvider.WarmPool(prefab, size);
        }
        
    }
}
