using Code.Core.Factory;
using Code.Core.ObjectPool;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using Vector3 = UnityEngine.Vector3;

namespace Code.Logic.Particles
{
    public class SoulLootParticle : SerializedMonoBehaviour
    {
        [SerializeField] private ParticleSystem _soulParticle;
        [SerializeField] private IPoolable _poolable;

        private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[100];

        private Transform _targetTransform;

        private readonly float _speed = 3;
        
        [Inject]
        public void Construct(IHeroFactory heroFactory)
        {
            _targetTransform = heroFactory.HeroGameObject.transform;
        }

        private void LateUpdate()
        {
            var particlesCount = _soulParticle.GetParticles(_particles);

            float time = 0;
            for (int particle = 0; particle <= particlesCount; particle++)
            {
                time += Time.deltaTime;
                _particles[particle].position =
                    Vector3.Lerp(_particles[particle].position, _targetTransform.position, time * _speed);

                if (Vector3.Distance(_particles[particle].position, _targetTransform.position) <= 4)
                {
                    _particles[particle].remainingLifetime = -1;
                }
            }

            _soulParticle.SetParticles(_particles, particlesCount);
        }
    }
}
