using System;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.VisualEffects;
using UnityEngine;


namespace Code.Runtime.Logic.Projectiles
{
    public class Projectile : PooledObject
    {
        public event Action<CollisionData> OnHit;
        
        [SerializeField] private Collider _collider;

        [SerializeField] private VisualEffect _collisionEffect;
        
        private VisualEffectFactory _visualFactory;

        public void Construct(VisualEffectFactory visualFactory)
        {
            _visualFactory = visualFactory;
        }
        protected void HandleCollision(GameObject other)
        {
            if (_collisionEffect is not null)
            {
                VisualEffect collisionEffect;
            }
        }
    }
}