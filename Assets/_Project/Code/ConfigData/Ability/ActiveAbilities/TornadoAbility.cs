using Code.Core.Factory;
using Code.Core.ObjectPool;
using Code.Logic.EntitiesComponents;
using Code.Logic.Projectiles;
using Code.Services.BattleService;
using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    public class TornadoAbility : IAbility
    {
        private readonly ParticleFactory _particleFactory;
        private readonly IBattleService _battleService;
        private readonly VfxData _vfxData;
        private readonly float _duration;
        private readonly float _damage;
        private readonly float _attackRadius;
        private readonly float _castDistance;

        private ParticleSystem _tornadoParticle;
        
        public TornadoAbility(ParticleFactory particleFactory,
            IBattleService battleService,
            VfxData vfxData,
            float duration,
            float damage,
            float attackRadius,
            float castDistance)
        {
            _particleFactory = particleFactory;
            _battleService = battleService;
            _vfxData = vfxData;
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
            TornadoProjectile tornadoProjectile = _tornadoParticle.gameObject.GetComponent<TornadoProjectile>();
            tornadoProjectile.Initialize(_vfxData, _duration);
            Transform projectileTransform = tornadoProjectile.transform;
            projectileTransform.position = casterPosition + casterDirection * _castDistance;
            projectileTransform.rotation = Quaternion.identity;

            var entityAttack = caster.GetComponent<IAttack>();
            
            entityAttack.SkillAttack(projectileTransform.position);
            
        }
        
    }
}
