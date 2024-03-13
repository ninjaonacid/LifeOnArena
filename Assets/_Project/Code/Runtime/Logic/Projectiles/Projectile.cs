using System;
using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.Collision;
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
        [SerializeField] private VisualEffectIdentifier _collisionEffectId;
        [SerializeField] private LayerMask _mask;
        
        private VisualEffectFactory _visualFactory;
        private CancellationTokenSource _cts;
        
        [Inject]
        public void Construct(VisualEffectFactory visualFactory)
        {
            _visualFactory = visualFactory;
            
            if(_collisionEffectId != null)
                _visualFactory.PrewarmEffect(_collisionEffectId.Id, 1).Forget();
        }
        
        public Projectile SetVelocity(Vector3 direction, float speed)
        {
            Vector3 movementVector = new Vector3(direction.x, 0, direction.z);
            _rb.velocity = movementVector * speed;
            return this;
        }

        public Projectile SetLayerMask(LayerMask mask)
        {
            _mask = mask;
            return this;
        }

        public void SetLifetime(float lifeTime)
        {
            HandleLifetime(lifeTime, TaskHelper.CreateToken(ref _cts)).Forget();
        }

        public void OnTriggerEnter(Collider other)
        {
            if(_mask == 1 << other.gameObject.layer)
            {
                HandleCollision(other.gameObject);
            }
        }

        private void HandleCollision(GameObject other)
        {
            // if (_collisionEffectId is not null)
            // {
            //     VisualEffect collisionEffect = await _visualFactory.CreateVisualEffect(_collisionEffectId.Id);
            //     transform.position = other.transform.position;
            //     
            // }
            
            Debug.Log($"collided with: {other.gameObject.name}");
            
            OnHit?.Invoke(new CollisionData
            {
                Target = other
            });
            
            ReturnToPool();
        }

        private async UniTask HandleLifetime(float lifeTime, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifeTime), cancellationToken: token);
            ReturnToPool();
        }
    }
}