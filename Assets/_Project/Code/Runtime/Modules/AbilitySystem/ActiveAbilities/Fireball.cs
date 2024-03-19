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

        public override void Use(GameObject caster, GameObject target)
        {
            base.Use(caster, target);
        }

        public Fireball(ActiveAbilityBlueprintBase abilityBlueprint, Projectile projectilePrefab, float lifeTime, float speed) : base(abilityBlueprint, projectilePrefab, lifeTime, speed)
        {
        }
    }
}