using System;
using System.Numerics;
using Code.Data.PlayerData;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Logic.Particles
{
    public class SoulLoot : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _soulParticle;

        private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[100];
        
        private Transform _targetTransform;
        private PlayerData _playerData;
        private Loot _loot;

        public void Construct(PlayerData playerData, Transform heroTransform)
        {
            _targetTransform = heroTransform;
            _playerData = playerData;
        }
        
        private void Awake()
        {
            _soulParticle = GetComponent<ParticleSystem>();
        }

        private void LateUpdate()
        {
            var particlesCount = _soulParticle.GetParticles(_particles);

            float time = 0;
            for (int particle = 0; particle <= particlesCount; particle++)
            {
                time += Time.deltaTime;
                _particles[particle].position =
                    Vector3.Lerp(_particles[particle].position, _targetTransform.position, time / 2);

                if (Vector3.Distance(_particles[particle].position, _targetTransform.position) < 10)
                {
                    _particles[particle].remainingLifetime = 0;

                    _loot.Value /= particlesCount;
                    _playerData.WorldData.LootData.Collect(_loot);
                }
            }

            _soulParticle.SetParticles(_particles, particlesCount);
        }

        public void Initialize(Loot loot)
        {
            _loot = loot;
        }
    }
}
