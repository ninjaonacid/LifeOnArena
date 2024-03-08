using Code.Runtime.Core.Factory;
using Code.Runtime.Entity;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class Fireball : IAbility
    {
        private readonly Projectile _projectile;
        private readonly ProjectileFactory _projectileFactory;
        public Fireball(Projectile projectile, ProjectileFactory projectileFactory)
        {
            _projectile = projectile;
            _projectileFactory = projectileFactory;
        }
        public void Use(GameObject caster, GameObject target)
        {
            Projectile projectile = _projectileFactory.CreateProjectile(_projectile);
            var hurtBox = caster.GetComponent<EntityHurtBox>();
            Vector3 casterCenter = hurtBox.GetCenterTransform();
            projectile.transform.position = (casterCenter + Vector3.forward);
            var direction = caster.transform.forward;
            projectile.MoveProjectile(direction, 100);
        }
    }
}