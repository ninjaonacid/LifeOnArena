using Code.Runtime.Entity;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Projectiles;
using Code.Runtime.Logic.VisualEffects;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class ProjectileAbility : ActiveAbility
    {
        protected readonly Projectile _projectilePrefab;
        protected readonly float _lifeTime;
        protected readonly float _speed;

        public ProjectileAbility(ActiveAbilityBlueprintBase abilityBlueprint, Projectile projectilePrefab, float lifeTime, float speed) : base(abilityBlueprint)
        {
            _projectilePrefab = projectilePrefab;
            _lifeTime = lifeTime;
            _speed = speed;
        }

        public override void Use(AbilityController caster, GameObject target)
        {
            Projectile projectile = _projectileFactory
                .CreateProjectile(_projectilePrefab, caster.gameObject, OnCreate, OnRelease, OnGet, OnReturn);

            var entityAttack = caster.GetComponent<EntityAttackComponent>();
            var layer = entityAttack.GetTargetLayer();

            var hurtBox = caster.GetComponent<EntityHurtBox>();
            Vector3 casterCenter = hurtBox.GetCenterTransform();
            var direction = caster.transform.forward;
            projectile.transform.position = casterCenter + (2 * direction);

            projectile
                .SetVelocity(direction, _speed)
                .SetTargetLayer(layer)
                .SetOwnerLayer(caster.gameObject)
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

        protected void OnCreate(Projectile projectile)
        {
        }

        protected void OnReturn(Projectile projectile)
        {
            projectile.OnHit -= OnHit;
        }

        protected void OnRelease(Projectile projectile)
        {
        }

        protected void OnGet(Projectile projectile)
        {
            projectile.OnHit += OnHit;
            projectile.SetVelocity(Vector3.zero, 0);
        }
        
    }
}