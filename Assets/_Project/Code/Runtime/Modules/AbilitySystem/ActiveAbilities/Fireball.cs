using Code.Runtime.Core.Factory;
using Code.Runtime.Entity;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class Fireball : IAbility
    {
        private readonly Projectile _projectilePrefab;
        private readonly ProjectileFactory _projectileFactory;
        private readonly float _lifeTime;
        private readonly float _speed;

        public Fireball(Projectile projectilePrefab, ProjectileFactory projectileFactory, float lifeTime, float speed)
        {
            _projectilePrefab = projectilePrefab;
            _projectileFactory = projectileFactory;
            _lifeTime = lifeTime;
            _speed = speed;
        }

        public void Use(GameObject caster, GameObject target)
        {
            Projectile projectile = _projectileFactory.CreateProjectile(_projectilePrefab);
            var layer = caster.GetComponent<EntityAttack>().GetTargetLayer();
            var hurtBox = caster.GetComponent<EntityHurtBox>();
            Vector3 casterCenter = hurtBox.GetCenterTransform();
            projectile.transform.position = (casterCenter + Vector3.forward);
            var direction = caster.transform.forward;
            projectile.Setup(direction, _speed, _lifeTime, layer);
            projectile.OnHit += OnHit;
        }

        private void OnHit(CollisionData obj)
        {
            
        }
    }
}