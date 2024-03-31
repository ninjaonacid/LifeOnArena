using Code.Runtime.Entity;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class DotAoeAbility : AoeAbility
    {
        private readonly float _castDistance;
        private float _aoeRadius;
        
        public DotAoeAbility(ActiveAbilityBlueprintBase abilityBlueprint, float castDistance, float duration,
            float aoeRadius) : base(abilityBlueprint, castDistance, duration, aoeRadius)
        {
            _castDistance = castDistance;
            _aoeRadius = aoeRadius;
        }

        public override async void Use(GameObject caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;

            var visualEffect =
                await _visualEffectFactory.CreateVisualEffect(AbilityBlueprint.VisualEffectData.Identifier.Id);

            Transform visualEffectTransform = visualEffect.transform;
            var visualEffectPosition = casterPosition + casterDirection * _castDistance;
            visualEffectTransform.position = visualEffectPosition;
            visualEffectTransform.rotation = Quaternion.LookRotation(casterDirection);
            visualEffect.Play();
            
            var layer = caster.GetComponent<EntityAttack>().GetTargetLayer();
            var stats = caster.GetComponent<StatController>();
        }
    }
}