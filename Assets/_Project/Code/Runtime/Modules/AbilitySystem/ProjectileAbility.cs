using Code.Runtime.Core.Factory;
using Code.Runtime.Entity;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class ProjectileAbility : ActiveAbility
    {
        private Projectile _projectilePrefab;
        private float _lifeTime;
        private float _speed;

        public ProjectileAbility(ActiveAbilityBlueprintBase abilityBlueprint, Projectile projectilePrefab, float lifeTime, float speed) : base(abilityBlueprint)
        {
            _projectilePrefab = projectilePrefab;
            _lifeTime = lifeTime;
            _speed = speed;
        }

        public override void Use(GameObject caster, GameObject target)
        {
            Projectile projectile = _projectileFactory
                .CreateProjectile(_projectilePrefab, OnCreate, OnRelease, OnGet);

            var entityAttack = caster.GetComponent<EntityAttack>();
            var layer = entityAttack.GetTargetLayer();
            projectile.gameObject.layer = entityAttack.GetLayer();
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

        protected void OnCreate(Projectile projectile)
        {
            projectile.OnHit += OnHit;
        }

        protected void OnRelease(Projectile projectile)
        {
            projectile.OnHit -= OnHit;
        }

        protected void OnGet(Projectile projectile)
        {
            projectile.SetVelocity(Vector3.zero, 0);
        }
    }
}