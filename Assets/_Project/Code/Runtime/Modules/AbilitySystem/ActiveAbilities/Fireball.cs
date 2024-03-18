using System.Collections.Generic;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class Fireball : ActiveAbility
    {
        private readonly Projectile _projectilePrefab;
        private readonly ProjectileFactory _projectileFactory;
        private readonly float _lifeTime;
        private readonly float _speed;

        private Projectile _projectile;


        public Fireball(IReadOnlyList<GameplayEffect> effects, float cooldown, float activeTime, bool isCastAbility,
            Projectile projectilePrefab, ProjectileFactory projectileFactory, float lifeTime, float speed,
            Projectile projectile) : base(effects, cooldown, activeTime, isCastAbility)
        {
            _projectilePrefab = projectilePrefab;
            _effects = effects;
            _projectileFactory = projectileFactory;
            _lifeTime = lifeTime;
            _speed = speed;
            _projectile = projectile;
        }

        public override void Use(GameObject caster, GameObject target)
        {
            Projectile projectile = _projectileFactory
                .CreateProjectile(_projectilePrefab, OnCreate, OnRelease, OnGet);

            var layer = caster.GetComponent<EntityAttack>().GetTargetLayer();
            projectile.gameObject.layer = caster.GetComponent<EntityAttack>().GetLayer();
            var hurtBox = caster.GetComponent<EntityHurtBox>();
            Vector3 casterCenter = hurtBox.GetHeightTransform();
            projectile.transform.position = casterCenter;
            var direction = caster.transform.forward;

            projectile
                .SetVelocity(direction, _speed)
                .SetLayerMask(layer)
                .SetOwner(caster)
                .SetLifetime(_lifeTime);
        }

        private void OnHit(CollisionData data)
        {
            ApplyEffects(data.Target);

            data.Source.GetComponent<IAttackComponent>().InvokeHit(1);
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