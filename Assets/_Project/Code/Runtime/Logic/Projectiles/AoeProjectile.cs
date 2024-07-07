using Code.Runtime.Logic.Collision;
using UnityEngine;

namespace Code.Runtime.Logic.Projectiles
{
    public class AoeProjectile : Projectile
    {
        protected override void HandleCollision(GameObject other)
        {
            InvokeOnHit(new CollisionData
            {
                Source = _owner,
                Target = other
            });
        }
    }
}