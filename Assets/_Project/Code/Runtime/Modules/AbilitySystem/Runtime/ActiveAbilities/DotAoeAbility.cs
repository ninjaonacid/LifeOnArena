using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Entity;
using Code.Runtime.Entity.StatusEffects;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class DotAoeAbility : ActiveAbility
    {
        private readonly float _castDistance;

        public DotAoeAbility(ActiveAbilityBlueprintBase abilityBlueprint, float castDistance) : base(abilityBlueprint)
        {
            _castDistance = castDistance;
        }

        public override async void Use(AbilityController caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;

            var visualEffect =
                await _visualEffectFactory
                    .CreateVisualEffect(AbilityBlueprint.VisualEffectData.Identifier.Id,
                        caster.gameObject, OnCreate, OnRelease);

            Transform visualEffectTransform = visualEffect.transform;
            var visualEffectPosition = casterPosition + casterDirection * _castDistance;
            visualEffectTransform.position = visualEffectPosition;
            visualEffectTransform.rotation = Quaternion.LookRotation(casterDirection);
            visualEffect.Play();

            if (AbilityBlueprint.AbilitySound is not null)
            {
                _audioService.PlaySound3D(AbilityBlueprint.AbilitySound, visualEffectTransform, 1f);
            }

            var entityAttack = caster.GetComponent<EntityAttackComponent>();
            var layer = entityAttack.GetTargetLayer();
            var owner = entityAttack.GetLayer();

            var areaOfEffect = visualEffect.GetComponent<AreaOfEffect>();
            areaOfEffect
                .SetTargetLayer(layer)
                .SetOwnerLayer(owner);
        }

        private void EntityEnter(GameObject obj)
        {
            ApplyEffects(obj);
        }

        private void EntityExit(GameObject obj)
        {
            RemoveEffects(obj);
        }

        private void OnCreate(PooledObject visualObj)
        {
            var areaOfEffect = visualObj.GetComponent<AreaOfEffect>();
            areaOfEffect.OnEnter += EntityEnter;
            areaOfEffect.OnExit += EntityExit;
        }

        private void OnRelease(PooledObject visualObj)
        {
            var areaOfEffect = visualObj.GetComponent<AreaOfEffect>();
            areaOfEffect.OnEnter -= EntityEnter;
            areaOfEffect.OnExit -= EntityExit;
        }

        private void RemoveEffects(GameObject target)
        {
            var statusController = target.GetComponentInParent<StatusEffectController>();

            foreach (var effect in _gameplayEffects)
            {
                if (effect is DurationalGameplayEffect durationalEffect)
                {
                    statusController.AddEffectToRemove(durationalEffect);
                }
            }
        }
    }
}