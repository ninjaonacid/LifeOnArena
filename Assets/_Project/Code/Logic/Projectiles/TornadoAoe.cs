using System;
using Code.ConfigData;
using Code.Core.ObjectPool;
using Code.Services.BattleService;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Logic.Projectiles
{
    public class TornadoAoe : SerializedMonoBehaviour
    {
        private IBattleService _battleService;
        private float _damage;
        
        [SerializeField] private ParticleSystem _tornadoParticle;
        [SerializeField] private IPoolable _poolable;
        private LayerMask _hittable;
        
        public void Initialize(IBattleService battleService, float damage, float lifeTime)
        {
            _battleService = battleService;
            _damage = damage;
            
            ReturnToPoolTask(lifeTime).Forget();
        }

        private void Awake()
        {
            _hittable = LayerMask.NameToLayer("Hittable");
        }

        public void OnCollisionStay(UnityEngine.Collision other)
        {
            if (other.gameObject.layer == _hittable)
            {
                
            }
        }

        private async UniTask ReturnToPoolTask(float lifeTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifeTime));
            _poolable.ReturnToPool();
        }

        
    }
}
