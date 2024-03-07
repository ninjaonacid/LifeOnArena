using System;
using Code.Runtime.Logic.Collision;

namespace Code.Runtime.Logic.Projectiles
{
    public interface IProjectile
    {
        public event Action<CollisionData> OnHit;
    }
}