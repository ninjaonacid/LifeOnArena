using System;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.VisualEffects;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Code.Runtime.Logic.Projectiles
{
    public class Projectile : PooledObject
    {
        public event Action<CollisionData> OnHit;
        
        [SerializeField] private Collider _collider;

        [SerializeField] private VisualEffectIdentifier _visualEffectId;
        
        private VisualEffectFactory _visualFactory;

        public void Construct(VisualEffectFactory visualFactory)
        {
            _visualFactory = visualFactory;
            _visualFactory.PrewarmEffect(_visualEffectId.Id, 1).Forget();
        }

        public async void OnTriggerEnter(Collider other)
        {
            await HandleCollision(other.gameObject);
        }

        protected async UniTask HandleCollision(GameObject other)
        {
            if (_visualEffectId is not null)
            {
                VisualEffect collisionEffect = await _visualFactory.CreateVisualEffect(_visualEffectId.Id);
                transform.position = other.transform.position;
            }
            
            OnHit?.Invoke(new CollisionData
            {
                Target = other
            });
        }
    }
}