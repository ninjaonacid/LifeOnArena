using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Logic.Particles
{
    public class SoulParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _soulParticle;

        private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[100];

        private Transform _transform;
        private Transform _targetTransform;

        private void Awake()
        {
            _transform = transform;
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
                }
            }

            _soulParticle.SetParticles(_particles, particlesCount);
            
            
        }

        public void Initialize(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }
    }
}
