using Code.ConfigData.StatSystem;
using Code.Core.ObjectPool;
using Code.Logic.EntitiesComponents;
using Code.Logic.Projectiles;
using Code.Services.BattleService;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    public class TornadoAbility : IAbility
    {
        private readonly IParticleObjectPool _particlesPool;
        private readonly IBattleService _battleService;
        private readonly AssetReference _tornadoPrefab;
        private readonly float _duration;
        private readonly float _damage;
        private readonly float _attackRadius;
        private readonly float _castDistance;
        
        public TornadoAbility(IParticleObjectPool particlePool,
            IBattleService battleService,
            AssetReference tornadoPrefab,
            float duration,
            float damage,
            float attackRadius,
            float castDistance)
        {
            _particlesPool = particlePool;
            _battleService = battleService;
            _tornadoPrefab = tornadoPrefab;
            _duration = duration;
            _damage = damage;
            _attackRadius = attackRadius;
            _castDistance = castDistance;
        }
        public async void Use(GameObject caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;
            
            
            GameObject tornadoPrefab = await _particlesPool.GetObject(_tornadoPrefab);
            TornadoProjectile tornadoProjectile = tornadoPrefab.GetComponent<TornadoProjectile>();
            Transform projectileTransform = tornadoProjectile.transform;
            projectileTransform.position = casterPosition + casterDirection * _castDistance;
            projectileTransform.rotation = Quaternion.identity;

            var casterStats = caster.GetComponent<StatController>();

            var entityAttack = caster.GetComponent<IAttack>();
            
            entityAttack.SkillAttack(projectileTransform.position);
            

          
        }

    }
}
