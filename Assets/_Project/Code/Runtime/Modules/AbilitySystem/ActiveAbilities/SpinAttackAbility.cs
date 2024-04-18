﻿using Code.Runtime.Entity;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class SpinAttackAbility : ActiveAbility
    {
        public SpinAttackAbility(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
        }

        public override async void Use(GameObject caster, GameObject target)
        {
            var effect = await _visualEffectFactory.CreateVisualEffect(AbilityBlueprint.VisualEffectData.Identifier.Id);
            var effectTargetTransform = caster.GetComponent<EntityHurtBox>().GetCenterTransform();
            effect.transform.position = effectTargetTransform;
            effect.transform.SetParent(caster.transform);
        }
    }
}