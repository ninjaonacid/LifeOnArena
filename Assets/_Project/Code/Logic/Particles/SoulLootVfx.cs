using Code.Core.Factory;
using UnityEngine;
using VContainer;
using Vector3 = UnityEngine.Vector3;

namespace Code.Logic.Particles
{
    public class SoulLootVfx : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _soulParticle;

        private readonly ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[100];
        
        private Transform _targetTransform;

        [Inject]
        public void Construct(IHeroFactory heroFactory)
        {
            _targetTransform = heroFactory.HeroGameObject.transform;
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
                    Vector3.Lerp(_particles[particle].position, _targetTransform.position, time * 2);

                if (Vector3.Distance(_particles[particle].position, _targetTransform.position) < 5)
                {
                    _particles[particle].remainingLifetime = -1;
                }

                if (!_soulParticle.isPlaying)
                {
                    gameObject.SetActive(false);
                }
                
            }

            _soulParticle.SetParticles(_particles, particlesCount);
        }
        
        
    }
}
