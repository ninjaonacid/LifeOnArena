using System;
using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Entity;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.VisualEffects;
using Code.Runtime.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;


namespace Code.Runtime.Logic.Projectiles
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Projectile : PooledObject
    {
        public event Action<CollisionData> OnHit;
        
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private ParticleSystem _effect;

        private LayerMask _targetLayer;
        private GameObject _owner;
        
        private CancellationTokenSource _cts;

        public Projectile SetVelocity(Vector3 direction, float speed)
        {
            Vector3 movementVector = new Vector3(direction.x, 0, direction.z);
            _rb.velocity = movementVector * speed;
            return this;
        }

        public Projectile SetTargetLayer(LayerMask mask)
        {
            _targetLayer = mask;
            return this;
        }

        public Projectile SetOwnerLayer(GameObject owner)
        {
            var ownerLayer = owner.GetComponent<EntityAttackComponent>().GetLayer();
            gameObject.layer = ownerLayer;
            _owner = owner;
            return this;
        } 

        public void SetLifetime(float lifeTime)
        {
            HandleLifetime(lifeTime, TaskHelper.CreateToken(ref _cts)).Forget();

            if (_effect != null)
            {
                _effect.Play();
            }
            
        }

        public void OnTriggerEnter(Collider other)
        {
            var value = _targetLayer.value;
            var target = 1 << other.gameObject.layer;
            if(_targetLayer == 1 << other.gameObject.layer)
            {
                HandleCollision(other.gameObject);
            }
        }

        private void HandleCollision(GameObject other)
        {
            OnHit?.Invoke(new CollisionData
            {
                Source = _owner,
                Target = other
            });

            ReturnToPool();
        }
        
        
        private async UniTask HandleLifetime(float lifeTime, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                ReturnToPool();
                token.ThrowIfCancellationRequested();
            }
            await UniTask.Delay(TimeSpan.FromSeconds(lifeTime), cancellationToken: token);
            
            if(_effect != null)
                _effect.Stop();
            
            ReturnToPool();
        }
    }
}