using System;
using System.Threading.Tasks;
using Code.ConfigData.Identifiers;
using Code.Core.Factory;
using Code.Entity.Enemy;
using Code.Logic.Collision;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Logic.Weapon
{
    [RequireComponent(typeof(Collider))]
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _slashTrail;
        [SerializeField] private ParticleIdentifier _hitVfx;
        public event Action<CollisionData> Hit;
        private LayerMask _mask;
        private ParticleFactory _particleFactory;

        [Inject]
        public void Construct(ParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
            _particleFactory.PrewarmParticlePool(_hitVfx.Id, 5).Forget();
        }

        private void Awake()
        {
            _mask = LayerMask.NameToLayer("Hittable");
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (_mask.value == other.gameObject.layer)
            {
                Hit?.Invoke(new CollisionData()
                {
                    Target = other.gameObject
                }); 
                
                var vfx = await HitVfx();
               var hitbox = other.gameObject.GetComponent<EnemyHitBox>();
               vfx.gameObject.transform.position = other.transform.position + hitbox.GetHitBoxCenter();
               vfx.Play();
            }
        }

        private async UniTask<ParticleSystem> HitVfx()
        {
          var vfx = await _particleFactory.CreateParticle(_hitVfx.Id);
          return vfx;
        }

        public ParticleIdentifier GetHitVfx()
        {
            return _hitVfx;
        }

        public void EnableCollider(bool value)
        {
            _collider.enabled = value;
        }

        public void EnableTrail(bool value)
        {
            var slashTrailEmission = _slashTrail.emission;
            slashTrailEmission.enabled = value;
            
            if (value) _slashTrail.Play();
            else _slashTrail.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        }
    }
}
