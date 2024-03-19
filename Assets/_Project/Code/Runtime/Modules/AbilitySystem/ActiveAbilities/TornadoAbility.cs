using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Logic.Projectiles;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class TornadoAbility : ActiveAbility
    {
        private readonly float _duration;
        private readonly float _attackRadius;
        private readonly float _castDistance;

        public TornadoAbility(ActiveAbilityBlueprintBase abilityBlueprint, float duration, float attackRadius, float castDistance) : base(abilityBlueprint)
        {
            _duration = duration;
            _attackRadius = attackRadius;
            _castDistance = castDistance;
        }

        public override async void Use(GameObject caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;

            var tornadoVisual =
                await _visualEffectFactory.CreateVisualEffect(AbilityBlueprint.VisualEffectData.Identifier.Id)
                    as TornadoVisualEffect;

            tornadoVisual.Initialize(_duration);
            Transform projectileTransform = tornadoVisual.transform;
            var position = projectileTransform.position;
            position = casterPosition + casterDirection * _castDistance;
            projectileTransform.position = position;
            projectileTransform.rotation = Quaternion.identity;

            LayerMask mask = 1 << LayerMask.NameToLayer("Hittable");


            var stats = caster.GetComponent<StatController>();
            

            var targets = _battleService.GetTargetsInRadius(position, _attackRadius, mask);

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