using System.Collections.Generic;
using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.VisualEffects;
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

        public async UniTask<VisualEffect> CreateVisualEffect(int id)
        {
            var particle = _configProvider.Particle(id);

            var prefab = await _assetProvider.Load<GameObject>(particle.PrefabReference);
            
            var particleSystem = _poolProvider.Spawn<VisualEffect>(id, prefab);

            return particleSystem;
        }
        
        public async UniTask<VisualEffect> CreateVisualEffect(int id, Vector3 position)
        {
           var visualEffect =  await CreateVisualEffect(id);
           visualEffect.transform.position = position;
           return visualEffect;
        }

        public async UniTask<VisualEffect> CreateVisualEffectWithTimer(int id, float time)
        {
            var visualEffect = await CreateVisualEffect(id);
            _poolProvider.ReturnWithTimer(id, visualEffect, time).Forget();
            return visualEffect;
        }

        public async UniTask<VisualEffect> CreateVisualEffectWithTimer(int id, Vector3 position, float time)
        {
            var visualEffect = await CreateVisualEffect(id);
            _poolProvider.ReturnWithTimer(id, visualEffect, time).Forget();
            visualEffect.transform.position = position;
            return visualEffect;
        }

        public async UniTask<VisualEffect> CreateVisualEffect(int id, Vector3 position, Transform parent)
        {
            var visualEffect = await CreateVisualEffect(id);
            Transform transform;
            (transform = visualEffect.transform).SetParent(parent);
            transform.position = position;
            return visualEffect;
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
