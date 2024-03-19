using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class Fireball : ProjectileAbility
    {
        public Fireball(IReadOnlyList<GameplayEffect> effects, AbilityIdentifier identifier, Sprite icon,
            float cooldown, float activeTime, bool isCastAbility, ProjectileFactory projectileFactory,
            Projectile projectilePrefab, float lifeTime, float speed) : base(effects, identifier, icon, cooldown,
            activeTime, isCastAbility, projectileFactory, projectilePrefab, lifeTime, speed)
        {
        }

        public override void Use(GameObject caster, GameObject target)
        {
            base.Use(caster, target);
        }
    }
}