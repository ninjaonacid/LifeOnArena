using System.Linq;
using Code.Runtime.Entity;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class AoeAbility : ActiveAbility
    {
        private readonly float _castDistance;
        private readonly float _aoeRadius;

        private EntityAttackComponent _attackComponent;
        private StatController _statComponent;
        private VisualEffectController _vfxController;
        
        public AoeAbility(ActiveAbilityBlueprintBase abilityBlueprint, float castDistance,  float aoeRadius) : base(abilityBlueprint)
        {
            _castDistance = castDistance;
            _aoeRadius = aoeRadius;
        }

        public override async void Use(AbilityController caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;

            _vfxController = caster.GetComponent<VisualEffectController>();
            
            var visualEffectPosition = casterPosition + casterDirection * _castDistance;

            var playPosition = AbilityBlueprint.VisualEffectData.PlayPosition;
           
            await _vfxController.PlayVisualEffect(AbilityBlueprint.VisualEffectData, position: visualEffectPosition );
            
            if (AbilityBlueprint.AbilitySound is not null)
            {
                _audioService.PlaySound3D(AbilityBlueprint.AbilitySound, visualEffectPosition, 1f);
            }

            var layer = caster.GetComponent<EntityAttackComponent>().GetTargetLayer();
            var stats = caster.GetComponent<StatController>();
            
            var targets = _battleService.GetTargetsInRadius(visualEffectPosition, _aoeRadius, layer);

            if (targets.hits > 0)
            {
                for (var index = 0; index < targets.hits; index++)
                {
                    var collider = targets.colliders[index];
                    ApplyEffects(collider.gameObject);
                }
            }

            var entityAttack = caster.GetComponent<IAttackComponent>();
            entityAttack.InvokeHit(targets.hits);
        }
    }
}