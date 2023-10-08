using System;
using UnityEngine;

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

            for (int particle = 0; particle <= particlesCount; particle++)
            {
                _particles[particle].position =
                    Vector3.Lerp(_particles[particle].position, _targetTransform.position, 1);
            }
            _soulParticle.SetParticles(_particles, particlesCount);
        }

        public void Initialize(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }
    }
}
