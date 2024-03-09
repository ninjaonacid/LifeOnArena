using System.Collections.Generic;
using Code.Runtime.ConfigData;
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
    public class TornadoAbility : IAbility
    {
        private readonly VisualEffectFactory _visualEffectFactory;
        private readonly VisualEffectData _visualEffectData;
        private readonly BattleService _battleService;
        private readonly float _duration;
        private readonly float _attackRadius;
        private readonly float _castDistance;
        private readonly IReadOnlyList<GameplayEffect> _statusEffects;

        private TornadoVisualEffect _tornadoVisual;
        
        public TornadoAbility(VisualEffectFactory visualEffectFactory,
            BattleService battleService,
            VisualEffectData visualEffectData,
            IReadOnlyList<GameplayEffect> statusEffects,
            float duration,
            float attackRadius,
            float castDistance)
        {
            _visualEffectFactory = visualEffectFactory;
            _battleService = battleService;
            _visualEffectData = visualEffectData;
            _statusEffects = statusEffects;
            _duration = duration;
            _attackRadius = attackRadius;
            _castDistance = castDistance;
        }
        public async void Use(GameObject caster, GameObject target)
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

            var hits = _battleService.CreateAoeAbility(stats, _statusEffects, projectileTransform.position, _attackRadius, mask);
            
            var entityAttack = caster.GetComponent<IAttackComponent>();
            entityAttack.InvokeHit(hits);
            
        }
        
    }
    
}
