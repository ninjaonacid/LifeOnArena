using Code.Runtime.Core.Factory;
using UnityEngine;
using VContainer;
using Vector3 = UnityEngine.Vector3;

namespace Code.Runtime.Logic.VisualEffects
{
    public class SoulLootEffect : VisualEffect
    {
        private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[100];

        private Transform _targetTransform;

        private readonly float _speed = 0.1f;
        
        [Inject]
        public void Construct(IHeroFactory heroFactory)
        {
            _targetTransform = heroFactory.HeroGameObject.transform;
        }

        private void LateUpdate()
        {
            var particlesCount = _particleSystem.GetParticles(_particles);

            float time = 0;
            for (int particle = 0; particle <= particlesCount; particle++)
            {
                time += Time.deltaTime;
                _particles[particle].position =
                    Vector3.MoveTowards(_particles[particle].position, _targetTransform.position, time * _speed);

                if (Vector3.Distance(_particles[particle].position, _targetTransform.position) <= 1)
                {
                    _particles[particle].remainingLifetime = -1;
                }
                
                
            }
            _particleSystem.SetParticles(_particles, particlesCount);

            if (_particleSystem.particleCount <= 0)
            {
                ReturnToPool();
            }
            
        }
    }
}
