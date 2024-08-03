using Code.Runtime.Entity;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class MultipleProjectileAbility : ProjectileAbility
    {
        private readonly int _numberOfProjectiles;

        public MultipleProjectileAbility(ActiveAbilityBlueprintBase abilityBlueprint, Projectile projectilePrefab,
            float lifeTime, float speed, float spawnDelay, bool isAutoTarget, int numberOfProjectiles) : base(
            abilityBlueprint, projectilePrefab, lifeTime, speed, spawnDelay, isAutoTarget)
        {
            _numberOfProjectiles = numberOfProjectiles;
        }

        public override void Use(AbilityController caster, GameObject target)
        {
            float angleStep = 360f / _numberOfProjectiles; // The angle between each projectile

            var entityAttack = caster.GetComponent<EntityAttackComponent>();
            var layer = entityAttack.GetTargetLayer();

            var hurtBox = caster.GetComponent<EntityHurtBox>();
            Vector3 casterCenter = hurtBox.GetCenterTransform();

            for (int i = 0; i < _numberOfProjectiles; i++)
            {
                float angle = i * angleStep; // The angle of the current projectile
                Vector3 direction =
                    Quaternion.Euler(0f, angle, 0f) * Vector3.forward; // The direction of the current projectile

                Projectile projectile = _projectileFactory
                    .CreateProjectile(_projectilePrefab, caster.gameObject, OnCreate, OnRelease, OnGet, OnReturn);

                projectile.transform.position = casterCenter + (2 * direction);

                projectile
                    .SetVelocity(direction, _speed)
                    .SetTargetLayer(layer)
                    .SetOwnerLayer(caster.gameObject)
                    .SetLifetime(_lifeTime);
            }
        }
    }
}