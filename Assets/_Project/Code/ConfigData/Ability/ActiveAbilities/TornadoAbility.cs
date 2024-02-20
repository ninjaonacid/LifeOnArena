using System.Collections.Generic;
using Code.Core.Factory;
using Code.Entity;
using Code.Entity.EntitiesComponents;
using Code.Entity.StatusEffects;
using Code.Logic.Projectiles;
using Code.Services.BattleService;
using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    public class TornadoAbility : IAbility
    {
        private readonly ParticleFactory _particleFactory;
        private readonly BattleService _battleService;
        private readonly VfxData _vfxData;
        private readonly float _duration;
        private readonly float _damage;
        private readonly float _attackRadius;
        private readonly float _castDistance;
        private readonly IReadOnlyList<StatusEffect> _statusEffects;

        private ParticleSystem _tornadoParticle;
        
        public TornadoAbility(ParticleFactory particleFactory,
            BattleService battleService,
            VfxData vfxData,
            IReadOnlyList<StatusEffect> statusEffects,
            float duration,
            float damage,
            float attackRadius,
            float castDistance)
        {
            _particleFactory = particleFactory;
            _battleService = battleService;
            _vfxData = vfxData;
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

            _tornadoParticle = await _particleFactory.CreateParticle(_vfxData.Identifier.Id);
            TornadoAoe tornadoAoe = _tornadoParticle.gameObject.GetComponent<TornadoAoe>();
            tornadoAoe.Initialize(_battleService, _damage, _duration);
            Transform projectileTransform = tornadoAoe.transform;
            projectileTransform.position = casterPosition + casterDirection * _castDistance;
            projectileTransform.rotation = Quaternion.identity;

            LayerMask mask = 1 << LayerMask.NameToLayer("Hittable");
            var targets = _battleService.FindTargets(projectileTransform.position, _attackRadius, mask);

            if (targets.Length > 0)
            {
                foreach (var targetCollider in targets)
                {
                    foreach (var effect in _statusEffects)
                    {
                        targetCollider.GetComponent<StatusEffectController>().ApplyEffectToSelf(effect);
                    }
                }
            }

            var entityAttack = caster.GetComponent<IAttack>();
            entityAttack.InvokeHit(targets.Length);
            
            
            //entityAttack.AoeAttack(projectileTransform.position);
            
        }
        
    }
    
}
