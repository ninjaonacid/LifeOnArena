using System;
using System.Collections;
using Code.ConfigData;
using Code.Core.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic.Projectiles
{
    public class TornadoProjectile : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _tornadoParticle;
        private ParticleObjectPool _pool;
        private ParticleObjectData _particleData;
        public void Initialize(ParticleObjectPool pool, ParticleObjectData data, float lifeTime)
        {
            _pool = pool;
            _particleData = data;
            
            //StartCoroutine(DestroyObjectCoroutine(lifeTime));
            ReturnToPoolTask(lifeTime).Forget();
            
        }

        private async UniTask ReturnToPoolTask(float lifeTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifeTime));
            _pool.ReturnObject(_particleData.Identifier.Id, _tornadoParticle);
        }

        
        public IEnumerator DestroyObjectCoroutine(float timeToDestroy)
        {
            yield return new WaitForSeconds(timeToDestroy);
            _pool.ReturnObject(_particleData.Identifier.Id, _tornadoParticle);
        }
    }
}
