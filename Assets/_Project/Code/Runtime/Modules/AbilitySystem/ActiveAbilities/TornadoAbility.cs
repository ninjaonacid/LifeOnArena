using System.Collections.Generic;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Logic.Projectiles;
using Code.Runtime.Logic.VisualEffects;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Services.BattleService;
using UnityEditor;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class TornadoAbility : ActiveAbility
    {
        private readonly VisualEffectFactory _visualEffectFactory;
        private readonly VisualEffectData _visualEffectData;
        private readonly BattleService _battleService;
        private readonly float _duration;
        private readonly float _attackRadius;
        private readonly float _castDistance;

        private TornadoVisualEffect _tornadoVisual;
        

        public override async void Use(GameObject caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;

            _tornadoVisual =
                await _visualEffectFactory.CreateVisualEffect(_visualEffectData.Identifier.Id)
                    as TornadoVisualEffect;

            _tornadoVisual.Initialize(_duration);
            Transform projectileTransform = _tornadoVisual.transform;
            projectileTransform.position = casterPosition + casterDirection * _castDistance;
            projectileTransform.rotation = Quaternion.identity;

            LayerMask mask = 1 << LayerMask.NameToLayer("Hittable");


            var stats = caster.GetComponent<StatController>();

            var hits = _battleService.CreateAoeAbility(stats, _effects, projectileTransform.position,
                _attackRadius, mask);

            var entityAttack = caster.GetComponent<IAttackComponent>();
            entityAttack.InvokeHit(hits);
        }
    }
}