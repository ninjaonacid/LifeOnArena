using System.Data;
using Code.Infrastructure.ObjectPool;
using Code.Logic.Abilities;
using Code.Services.BattleService;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.StaticData.Ability.ActiveAbilities
{
    public class Tornado : IAbility
    {
        private readonly IParticleObjectPool _particlesPool;
        private readonly IBattleService _battleService;
        private readonly AssetReference _tornadoVfx;
        private readonly float _duration;
        private readonly float _damage;
        private readonly float _attackRadius;

        private readonly LayerMask _layerMask = LayerMask.NameToLayer("Hittable");

        public Tornado(IParticleObjectPool particlePool,
            IBattleService battleService,
            AssetReference tornadoVfx,
            float duration,
            float damage,
            float attackRadius)
        {
            _particlesPool = particlePool;
            _battleService = battleService;
            _tornadoVfx = tornadoVfx;
            _duration = duration;
            _damage = damage;
            _attackRadius = attackRadius;
        }
        public async void Use(GameObject caster, GameObject target)
        {
            var casterPosition = caster.transform.position;
            var casterDirection = caster.transform.forward;
            var castOffset = 3f;

            var tornadoPrefab = await _particlesPool.GetObject(_tornadoVfx);
            var tornadoProjectile = tornadoPrefab.GetComponent<TornadoProjectile>();
            var projectileTransform = tornadoProjectile.transform;
            projectileTransform.position = casterPosition + casterDirection * castOffset;
            projectileTransform.rotation = Quaternion.identity;
            _battleService.AoeAttack(_damage, _attackRadius, 10,  projectileTransform.position, _layerMask);

            

            //Object.Instantiate(_tornadoVfx, casterPosition + casterDirection * castOffset, Quaternion.identity);
        }
    }
}
