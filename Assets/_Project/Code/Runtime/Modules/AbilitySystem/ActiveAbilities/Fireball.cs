using Code.Runtime.Core.Factory;
using Code.Runtime.Entity;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class Fireball : IAbility
    {
        private readonly Projectile _projectilePrefab;
        private readonly ProjectileFactory _projectileFactory;
        private readonly float _lifeTime;
        public Fireball(Projectile projectilePrefab, ProjectileFactory projectileFactory)
        {
            _projectilePrefab = projectilePrefab;
            _projectileFactory = projectileFactory;
        }
        public void Use(GameObject caster, GameObject target)
        {
            Projectile projectile = _projectileFactory.CreateProjectile(_projectilePrefab);
            var hurtBox = caster.GetComponent<EntityHurtBox>();
            Vector3 casterCenter = hurtBox.GetCenterTransform();
            projectile.transform.position = (casterCenter + Vector3.forward);
            var direction = caster.transform.forward;
            projectile.SetVelocity(direction, 50);
            
        }
    }
}