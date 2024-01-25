using System;
using Code.ConfigData;
using Code.Core.ObjectPool;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Logic.Projectiles
{
    public class TornadoProjectile : SerializedMonoBehaviour
    {
        [SerializeField] private ParticleSystem _tornadoParticle;
        [SerializeField] private IPoolable _poolable;
        private ParticleObjectData _particleData;
        public void Initialize(ParticleObjectData data, float lifeTime)
        {
            _particleData = data;
            
            ReturnToPoolTask(lifeTime).Forget();
        }

        private async UniTask ReturnToPoolTask(float lifeTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifeTime));
            _poolable.ReturnToPool();
        }

        
    }
}
