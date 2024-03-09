using System;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.VisualEffects;
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
        
        [Inject]
        public void Construct(VisualEffectFactory visualFactory)
        {
            _visualFactory = visualFactory;
            
            if(_collisionEffectId != null)
                _visualFactory.PrewarmEffect(_collisionEffectId.Id, 1).Forget();
        }

        public void Setup(Vector3 direction, float speed, float lifeTime, LayerMask mask)
        {
            Vector3 movementVector = new Vector3(direction.x, 0, direction.z);
            _rb.velocity = movementVector * speed;
            _mask = mask;

            HandleLifetime(lifeTime).Forget();
        }

        public async void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _mask)
            {
                await HandleCollision(other.gameObject);
            }
        }

        public void SetVelocity(Vector3 direction, float speed)
        {
            Vector3 movementVector = new Vector3(direction.x, 0, direction.z);
            _rb.velocity = movementVector * speed;
        }

        private async UniTask HandleCollision(GameObject other)
        {
            if (_collisionEffectId is not null)
            {
                VisualEffect collisionEffect = await _visualFactory.CreateVisualEffect(_collisionEffectId.Id);
                transform.position = other.transform.position;
            }
            
            OnHit?.Invoke(new CollisionData
            {
                Target = other
            });
        }

        private async UniTask HandleLifetime(float lifeTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifeTime));
            ReturnToPool();
        }
    }
}