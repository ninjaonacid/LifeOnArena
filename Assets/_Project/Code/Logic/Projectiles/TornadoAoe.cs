using System;
using System.Collections.Generic;
using Code.Core.ObjectPool;
using Code.Services.BattleService;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Logic.Projectiles
{
    public class TornadoAoe : SerializedMonoBehaviour
    {
        
        private BattleService _battleService;
        private float _damage;
        
        [SerializeField] private ParticleSystem _tornadoParticle;
        [SerializeField] private IPoolable _poolable;

        private List<Collider> _collidersInRadius;
        private LayerMask _hittable;
        
        public void Initialize(BattleService battleService, float damage, float lifeTime)
        {
            _battleService = battleService;
            _damage = damage;
            
            ReturnToPoolTask(lifeTime).Forget();
        }

        private void Awake()
        {
            _hittable = LayerMask.NameToLayer("Hittable");
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _hittable)
            {
                _collidersInRadius.Add(other);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == _hittable)
            {
                _collidersInRadius.Remove(other);
            }
        }

        private async UniTask ReturnToPoolTask(float lifeTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifeTime));
            _poolable.ReturnToPool();
        }

        private async UniTask DamageOverTimeTask(float damage, float timer)
        {
            foreach (var collider in _collidersInRadius)
            {
                
            }
        }


    }
}
