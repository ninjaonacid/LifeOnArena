using Code.Runtime.Entity;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class AoeAbility : ActiveAbility
    {
        private float _castDistance;
        private float _duration;
        private float _aoeRadius;
        
        public AoeAbility(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
        }

        public override async void Use(GameObject caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;

            var visualEffect =
                await _visualEffectFactory.CreateVisualEffect(AbilityBlueprint.VisualEffectData.Identifier.Id);
            
            Transform visualEffectTransform = visualEffect.transform;
            var position = visualEffectTransform.position;
            position = casterPosition + casterDirection * _castDistance;
            visualEffectTransform.position = position;
            visualEffectTransform.rotation = Quaternion.identity;

            var layer = caster.GetComponent<EntityAttack>().GetTargetLayer();
            var stats = caster.GetComponent<StatController>();
            
            var targets = _battleService.GetTargetsInRadius(position, _aoeRadius, layer);

            if (targets.hits > 0)
            {
                foreach (var collider in targets.colliders)
                {
                    ApplyEffects(collider.gameObject);
                }
            }

            var entityAttack = caster.GetComponent<IAttackComponent>();
            entityAttack.InvokeHit(targets.hits);
        }
    }
}