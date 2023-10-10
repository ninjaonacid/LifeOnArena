using System;
using Code.ConfigData.StatSystem;
using Code.Core.ObjectPool;
using Code.Logic.EntitiesComponents;
using Code.Logic.Projectiles;
using Code.Services.BattleService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    public class TornadoAbility : IAbility
    {
        private readonly ParticleObjectPool _particlesPool;
        private readonly IBattleService _battleService;
        private readonly ParticleData _particleData;
        private readonly float _duration;
        private readonly float _damage;
        private readonly float _attackRadius;
        private readonly float _castDistance;

        private GameObject _tornadoPrefab;
        
        public TornadoAbility(ParticleObjectPool particlePool,
            IBattleService battleService,
            ParticleData particleData,
            float duration,
            float damage,
            float attackRadius,
            float castDistance)
        {
            _particlesPool = particlePool;
            _battleService = battleService;
            _particleData = particleData;
            _duration = duration;
            _damage = damage;
            _attackRadius = attackRadius;
            _castDistance = castDistance;
        }
        public async void Use(GameObject caster, GameObject target)
        {
            Vector3 casterPosition = caster.transform.position;
            Vector3 casterDirection = caster.transform.forward;
            
            
            _tornadoPrefab = await _particlesPool.GetObject(_particleData.Identifier.Id);
            TornadoProjectile tornadoProjectile = _tornadoPrefab.GetComponent<TornadoProjectile>();
            Transform projectileTransform = tornadoProjectile.transform;
            projectileTransform.position = casterPosition + casterDirection * _castDistance;
            projectileTransform.rotation = Quaternion.identity;

            var casterStats = caster.GetComponent<StatController>();

            var entityAttack = caster.GetComponent<IAttack>();
            
            entityAttack.SkillAttack(projectileTransform.position);

             AbilityTimer().Forget();

        }

        private async UniTask AbilityTimer()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_duration));
            _particlesPool.ReturnObject(_particleData.Identifier.Id, _tornadoPrefab);
        }

    }
}
