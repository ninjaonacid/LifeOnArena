using Code.Runtime.Core.Factory;
using Code.Runtime.Logic.Projectiles;
using Code.Runtime.Modules.AbilitySystem.ActiveAbilities;

namespace Code.Runtime.Core.Builders
{
    public class FireballBuilder : AbilityBuilder<Fireball>
    {
        private readonly Projectile _projectilePrefab;
        private ProjectileFactory _projectileFactory;
        private readonly float _lifeTime;
        private readonly float _speed;

        public FireballBuilder ProjectileFactory(ProjectileFactory factory)
        {
            _projectileFactory = factory;
            return null;
        }
    }
}