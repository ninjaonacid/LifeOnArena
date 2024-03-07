using Code.Runtime.Core.Factory;
using Code.Runtime.Entity;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class Fireball : IAbility
    {
        private Projectile _projectile;
        private ProjectileFactory _projectileFactory;
        public Fireball(Projectile projectile, ProjectileFactory projectileFactory)
        {
            _projectile = projectile;
            _projectileFactory = projectileFactory;
        }
        public void Use(GameObject caster, GameObject target)
        {
            var projectile = _projectileFactory.CreateProjectile(_projectile);
            
        }
    }
}