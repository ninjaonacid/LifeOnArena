using System.Collections.Generic;
using Code.Runtime.ConfigData;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Logic.Projectiles;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Services.BattleService;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class TornadoAbility : IAbility
    {
        private readonly VisualEffectFactory _visualEffectFactory;
        private readonly BattleService _battleService;
        private readonly VisualEffectData _visualEffectData;
        private readonly float _duration;
        private readonly float _damage;
        private readonly float _attackRadius;
        private readonly float _castDistance;
        private readonly IReadOnlyList<GameplayEffect> _statusEffects;

        private ParticleSystem _tornadoParticle;
        
        public TornadoAbility(VisualEffectFactory visualEffectFactory,
            BattleService battleService,
            VisualEffectData visualEffectData,
            IReadOnlyList<GameplayEffect> statusEffects,
            float duration,
            float damage,
            float attackRadius,
            float castDistance)
        {
            _visualEffectFactory = visualEffectFactory;
            _battleService = battleService;
            _visualEffectData = visualEffectData;
            _statusEffects = statusEffects;
            _duration = duration;
            _damage = damage;
            _attackRadius = attackRadius;
            _castDistance = castDistance;
        }
        public async void Use(GameObject caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;

            _tornadoParticle = await _visualEffectFactory.CreateVisualEffect(_visualEffectData.Identifier.Id);
            TornadoAoe tornadoAoe = _tornadoParticle.gameObject.GetComponent<TornadoAoe>();
            tornadoAoe.Initialize(_battleService, _damage, _duration);
            Transform projectileTransform = tornadoAoe.transform;
            projectileTransform.position = casterPosition + casterDirection * _castDistance;
            projectileTransform.rotation = Quaternion.identity;

            LayerMask mask = 1 << LayerMask.NameToLayer("Hittable");

            var stats = caster.GetComponent<StatController>();

            int hits = _battleService.CreateAoeAbility(stats, _statusEffects, projectileTransform.position, _attackRadius, mask);
            
            var entityAttack = caster.GetComponent<IAttack>();
            entityAttack.InvokeHit(hits);
            
        }
        
    }
    
}
