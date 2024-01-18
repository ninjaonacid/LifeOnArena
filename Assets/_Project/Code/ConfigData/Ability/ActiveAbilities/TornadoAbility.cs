using Code.Core.ObjectPool;
using Code.Logic.EntitiesComponents;
using Code.Logic.Projectiles;
using Code.Services.BattleService;
using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    public class TornadoAbility : IAbility
    {
        private readonly ParticleObjectPool _particleObjectPool;
        private readonly IBattleService _battleService;
        private readonly ParticleObjectData _particleObjectData;
        private readonly float _duration;
        private readonly float _damage;
        private readonly float _attackRadius;
        private readonly float _castDistance;

        private ParticleSystem _tornadoParticle;
        
        public TornadoAbility(ParticleObjectPool particleObjectPool,
            IBattleService battleService,
            ParticleObjectData particleObjectData,
            float duration,
            float damage,
            float attackRadius,
            float castDistance)
        {
            _particleObjectPool = particleObjectPool;
            _battleService = battleService;
            _particleObjectData = particleObjectData;
            _duration = duration;
            _damage = damage;
            _attackRadius = attackRadius;
            _castDistance = castDistance;
        }
        public async void Use(GameObject caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;
            
            _tornadoParticle = await _particleObjectPool.GetObject(_particleObjectData.Identifier.Id);
            TornadoProjectile tornadoProjectile = _tornadoParticle.gameObject.GetComponent<TornadoProjectile>();
            tornadoProjectile.Initialize(_particleObjectPool, _particleObjectData, _duration);
            Transform projectileTransform = tornadoProjectile.transform;
            projectileTransform.position = casterPosition + casterDirection * _castDistance;
            projectileTransform.rotation = Quaternion.identity;

            var entityAttack = caster.GetComponent<IAttack>();
            
            entityAttack.SkillAttack(projectileTransform.position);
            
        }
        
    }
}
