using Code.Runtime.Entity;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Projectiles;
using Code.Runtime.Logic.VisualEffects;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class ProjectileAbility : ActiveAbility
    {
        private readonly Projectile _projectilePrefab;
        private readonly float _lifeTime;
        private readonly float _speed;

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

            var entityAttack = caster.GetComponent<EntityAttackComponent>();
            var layer = entityAttack.GetTargetLayer();

            var hurtBox = caster.GetComponent<EntityHurtBox>();
            Vector3 casterCenter = hurtBox.GetHeightTransform();
            projectile.transform.position = casterCenter;
            var direction = caster.transform.forward;

            projectile
                .SetVelocity(direction, _speed)
                .SetTargetLayer(layer)
                .SetOwnerLayer(caster)
                .SetLifetime(_lifeTime);
        }

        private async void OnHit(CollisionData data)
        {
            ApplyEffects(data.Target);
            
            if (AbilityBlueprint.VisualEffectData is not null)
            {
                VisualEffect collisionEffect = await _visualEffectFactory.CreateVisualEffect(AbilityBlueprint.VisualEffectData.Identifier.Id);
                collisionEffect.transform.position = data.Target.GetComponentInParent<EntityHurtBox>().GetCenterTransform();
                collisionEffect.Play();
            }

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