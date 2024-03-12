using System.Collections.Generic;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class Fireball : IAbility
    {
        private readonly Projectile _projectilePrefab;
        private readonly IReadOnlyList<GameplayEffect> _effects;
        private readonly ProjectileFactory _projectileFactory;
        private readonly float _lifeTime;
        private readonly float _speed;

        private Projectile _projectile;

        public Fireball(Projectile projectilePrefab, IReadOnlyList<GameplayEffect> effects,
            ProjectileFactory projectileFactory, float lifeTime, float speed)
        {
            _projectilePrefab = projectilePrefab;
            _effects = effects;
            _projectileFactory = projectileFactory;
            _lifeTime = lifeTime;
            _speed = speed;
        }

        public void Use(GameObject caster, GameObject target)
        {
            Projectile projectile = _projectileFactory
                .CreateProjectile(_projectilePrefab, OnCreate, OnRelease, OnGet);
            
            var layer = caster.GetComponent<EntityAttack>().GetTargetLayer();
            var hurtBox = caster.GetComponent<EntityHurtBox>();
            Vector3 casterCenter = hurtBox.GetCenterTransform();
            projectile.transform.position = (casterCenter + Vector3.forward + Vector3.up);
            var direction = caster.transform.forward;

            projectile
                .SetVelocity(direction, _speed)
                .SetLayerMask(layer)
                .SetLifetime(_lifeTime);
        }

        private void OnHit(CollisionData obj)
        {
            var statusController = obj.Target.GetComponent<StatusEffectController>();

            foreach (var effect in _effects)
            {
                statusController.ApplyEffectToSelf(effect);
            }
        }

        private void OnCreate(Projectile projectile)
        {
            projectile.OnHit += OnHit;
        }

        private void OnRelease(Projectile projectile)
        {
            projectile.OnHit -= OnHit;
        }

        private void OnGet(Projectile projectile)
        {
            projectile.SetVelocity(Vector3.zero, 0);
        }
    }
}